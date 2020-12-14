using System;
using System.Collections.Generic;
using System.Linq;
using Trustpilot.Pony.Core.Model;

namespace Trustpilot.Pony.Core
{
    internal class MazeDirectionFinder
    {
        /// <summary>
        /// Finds the available directions you can go from <param name="currentLocation"></param>
        /// </summary>
        /// <param name="maze">The current maze including wall data</param>
        /// <param name="currentLocation">The current player location</param>
        /// <returns>A list of directions that are available</returns>
        public IEnumerable<Direction> GetAvailableDirections(Maze maze, int currentLocation)
        {
            if (maze?.Data == null)
                throw new ArgumentNullException(nameof(maze));
            if (currentLocation < 0)
                throw new ArgumentOutOfRangeException(nameof(currentLocation));
            if (currentLocation + 1 > maze.Data.Length)
                throw new ArgumentOutOfRangeException(nameof(currentLocation));

            var available = new List<Direction>
            {
                Direction.North,
                Direction.West,
                Direction.East,
                Direction.South
            };

            var p1 = maze.Data[currentLocation];
            // If north
            //   North is out
            // If west
            //   West is out
            if (p1.Contains(Direction.North))
            {
                available.Remove(Direction.North);
            }
            if (p1.Contains(Direction.West))
            {
                available.Remove(Direction.West);
            }

            if (currentLocation + 1 < maze.Data.Length)
            {
                var p2 = maze.Data[currentLocation + 1];
                // If west
                //   East is out
                if (p2.Contains(Direction.West))
                {
                    available.Remove(Direction.East);
                }
            }
            else
            {
                available.Remove(Direction.East);
            }

            if (currentLocation + maze.Size.Width < maze.Data.Length)
            {
                var p3 = maze.Data[currentLocation + maze.Size.Width];
                // If North
                //   South is out
                if (p3.Contains(Direction.North))
                {
                    available.Remove(Direction.South);
                }
            }
            else
            {
                // Cannot go off the map
                available.Remove(Direction.South);
            }

            return available;
        }
    }
}
