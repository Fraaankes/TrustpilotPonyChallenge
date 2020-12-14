using System;
using Trustpilot.Pony.Core.Model;

namespace Trustpilot.Pony.Core
{
    internal class MazeMoveTranslator
    {
        /// <summary>
        /// Based on the <param name="direction"></param> will return the new location
        /// </summary>
        /// <param name="maze">The current maze</param>
        /// <param name="currentLocation">The current location</param>
        /// <param name="direction">The direction in which to move</param>
        /// <returns></returns>
        public int TranslateMove(Maze maze, int currentLocation, Direction direction)
        {
            return direction switch
            {
                Direction.Stay => currentLocation,
                Direction.North => currentLocation - maze.Size.Width,
                Direction.East => currentLocation + 1,
                Direction.West => currentLocation - 1,
                Direction.South => currentLocation + maze.Size.Width,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}
