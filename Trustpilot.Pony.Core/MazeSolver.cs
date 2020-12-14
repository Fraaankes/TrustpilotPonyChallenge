using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Trustpilot.Pony.Core.Model;

[assembly: InternalsVisibleTo("Trustpilot.Pony.Tests")]

namespace Trustpilot.Pony.Core
{
    public class MazeSolver
    {
        private readonly MazeDirectionFinder _directionFinder;
        private readonly MazeMoveTranslator _moveTranslator;

        public MazeSolver()
        {
            _directionFinder = new MazeDirectionFinder();
            _moveTranslator = new MazeMoveTranslator();
        }

        /// <summary>
        /// Returns a list of moves necessary to complete the maze
        /// For each move made, the maze will be retrieved using <param name="mazeDataGetter"></param> to re-evaluate the solution in case Domo moves into the poath
        /// </summary>
        /// <param name="mazeDataGetter">Function that returns updated location data for the maze</param>
        /// <returns></returns>
        public IAsyncEnumerable<MazeMove> Solve(Func<Task<Maze>> mazeDataGetter)
        {
            return new StateBasedMazeSolver(this, mazeDataGetter);
        }

        private class StateBasedMazeSolver : IAsyncEnumerator<MazeMove>, IAsyncEnumerable<MazeMove>
        {
            private readonly MazeSolver _solver;
            private readonly Func<Task<Maze>> _mazeDataGetter;
            private MazeMove _nextMove;

            public StateBasedMazeSolver(MazeSolver solver, Func<Task<Maze>> mazeDataGetter)
            {
                _solver = solver;
                _mazeDataGetter = mazeDataGetter;
            }
            
            public async ValueTask<bool> MoveNextAsync()
            {
                var maze = await _mazeDataGetter();
                var result = new List<MazeMove>();
                var visited = new bool[maze.Data.Length];
                visited[maze.PonyLocation] = true;
                var solved = _solver.SolveInternal(maze, visited, maze.PonyLocation, result);

                if (!solved || !result.Any())
                {
                    _nextMove = new MazeMove { Direction = Direction.Stay };
                }
                else
                {
                    _nextMove = result.First();
                }

                return true;
            }

            MazeMove IAsyncEnumerator<MazeMove>.Current => _nextMove;

            public ValueTask DisposeAsync()
            {
                return new ValueTask();
            }

            public IAsyncEnumerator<MazeMove> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
            {
                return this;
            }
        }

        private bool SolveInternal(Maze maze, bool[] visited, int location, List<MazeMove> result)
        {
            // Base case - success
            if (location == maze.Endpoint)
            {
                return true;
            }

            // Avoid Domo - discard the path
            if (location == maze.DomokunLocation)
            {
                return false;
            }

            // Figure out which directions are available from current location
            var availableDirection = _directionFinder.GetAvailableDirections(maze, location);

            // Use a depth first search to discover if the current direction leads to a way out
            foreach (var direction in availableDirection)
            {
                var nextLocation = _moveTranslator.TranslateMove(maze, location, direction);
                if (!visited[nextLocation])
                {
                    result.Add(new MazeMove {Direction = direction});
                    visited[nextLocation] = true;

                    if (SolveInternal(maze, visited, nextLocation, result))
                    {
                        return true;
                    }

                    // Path didn't work - discard result and reset the visited status
                    visited[nextLocation] = false;
                    result.RemoveAt(result.Count - 1);
                }
            }

            return false;
        }
    }
}