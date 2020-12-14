using System.Threading.Tasks;
using Shouldly;
using Trustpilot.Pony.Core;
using Trustpilot.Pony.Core.Model;
using Xunit;
using static Trustpilot.Pony.Core.Model.Direction;

namespace Trustpilot.Pony.Tests
{
    public class MazeSolverTests
    {
        private readonly MazeMoveTranslator _mazeMoveTranslator;

        public MazeSolverTests()
        {
            _mazeMoveTranslator = new MazeMoveTranslator();
        }

        [Fact]
        public async Task Maze_Is_Solved()
        {
            var expectedSteps = new[]
            {
                North,
                East,
                South,
                East,
                North,
                East,
                South,
                East,
                North,
                North,
                North,
                North,
                West,
                North,
                East,
                North,
                North,
                West,
                South,
                West,
                West,
                West,
                West,
                North,
                West,
                South,
                West,
                North,
                North,
                East,
                North,
                West,
                West,
                North,
                West,
                West,
                North,
                West,
                North,
                West,
                North,
                East,
                East,
                North,
                East,
                East,
                East,
                South,
                South,
                South,
                South,
                East,
                East,
                East,
                North,
                North,
                East,
                South,
                East,
                East,
                East,
                South
            };
            
            var maze = TestMazeData.LoadCompleteTestMaze();
            var solver = new MazeSolver();

            var enumerator = solver.Solve(() => Task.FromResult(maze)).GetAsyncEnumerator();
            var step = 0;
            
            while (await enumerator.MoveNextAsync())
            {
                var move = enumerator.Current;
                
                move.ShouldNotBeNull();

                if (maze.PonyLocation == maze.Endpoint)
                {
                    move.Direction.ShouldBe(Stay);
                    break;
                }

                move.Direction.ShouldBe(expectedSteps[step], $"Direction at step {step} was not expected");
                step++;
                
                SimulateMoving(maze, move);
            }
        }

        private void SimulateMoving(Maze maze, MazeMove move)
        {
            maze.PonyLocation = _mazeMoveTranslator.TranslateMove(maze, maze.PonyLocation, move.Direction);
        }
    }
}
