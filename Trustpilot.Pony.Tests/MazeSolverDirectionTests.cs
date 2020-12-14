using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Trustpilot.Pony.Core;
using Trustpilot.Pony.Core.Model;
using Xunit;

namespace Trustpilot.Pony.Tests
{
    public class MazeSolverDirectionTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(1024)]
        public void OutOfRange_Location_Throws(int location)
        {
            // Arrange
            var service = new MazeDirectionFinder();
            var maze = TestMazeData.LoadCompleteTestMaze();

            // Act
            void Act() => service.GetAvailableDirections(maze, location);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(Act);
        }

        [Fact]
        public void Null_Maze_Throws()
        {
            // Arrange
            var service = new MazeDirectionFinder();

            // Act
            void Act() => service.GetAvailableDirections(null, 0);

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void Null_Maze_Data_Throws()
        {
            // Arrange
            var service = new MazeDirectionFinder();

            // Act
            void Act() => service.GetAvailableDirections(new Maze(), 0);

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Theory]
        [InlineData(0, new[] {Direction.South})] // Top left
        [InlineData(14, new[] {Direction.West, Direction.South})] // Top right
        [InlineData(15, new[] {Direction.North, Direction.East})] // 1 row after top left
        [InlineData(35, new[] {Direction.North, Direction.East, Direction.South, Direction.West})] // Marked with A in txt
        [InlineData(220, new[] {Direction.West, Direction.North})] // Pony
        [InlineData(108, new[] {Direction.North, Direction.South})] // Domokun
        public void Moves_From_Location_Matches(int location, IEnumerable<Direction> expectedDirections)
        {
            // Arrange
            var service = new MazeDirectionFinder();
            var maze = TestMazeData.LoadCompleteTestMaze();

            // Act
            var directions = service.GetAvailableDirections(maze, location).ToList();

            // Assert
            foreach (var expectedDirection in expectedDirections)
            {
                directions.ShouldContain(expectedDirection, $"Directions did not contain {expectedDirection}");
            }
        }
    }
}
