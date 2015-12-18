using System;
using NUnit.Framework;

namespace AdventOfCode.Day18.Tests
{
    [TestFixture]
    public class Day18Tests
    {
        private const string Example_Initial = @".#.#.#
...##.
#....#
..#...
#.#..#
####..";

        private const string Example_1 = @"..##..
..##.#
...##.
......
#.....
#.##..";

        private const string Example_2 = @"..###.
......
..###.
......
.#....
.#....";

        private const string Example_3 = @"...#..
......
...#..
..##..
......
......";

        private const string Example_4 = @"......
......
..##..
..##..
......
......";


        [TestCase(Example_Initial, 1, Example_1)]
        [TestCase(Example_Initial, 2, Example_2)]
        [TestCase(Example_Initial, 3, Example_3)]
        [TestCase(Example_Initial, 4, Example_4)]
        public void RunBoardAnimation(string initialLayout, int stepCount, string expectedOutput)
        {
            var startingBoard = new Board(6);
            startingBoard.SetBoardValues(initialLayout);
            var actual = Processor.RunBoardAnimation(startingBoard, stepCount);

            Assert.AreEqual(expectedOutput, actual.ToString());
        }

        [TestCase(true, 2, true)]
        [TestCase(true, 3, true)]
        [TestCase(true, 4, false)]
        [TestCase(true, 1, false)]
        [TestCase(false, 3, true)]
        [TestCase(false, 1, false)]
        public void CalculateNewValueForLight(bool currentValue, int litNeighborCount, bool expectedNewValue)
        {
            var actual = Processor.CalculateNewValueForLight(currentValue, litNeighborCount);
            Assert.AreEqual(expectedNewValue, actual);
        }

        [TestCase(Example_3, 0, 3, 0)]
        [TestCase(Example_3, 1, 3, 2)]
        [TestCase(Example_2, 5, 0, 2)]
        [TestCase(Example_2, 5, 5, 0)]
        [TestCase(Example_4, 2, 2, 3)]
        [TestCase(Example_1, 1, 4, 5)]
        [TestCase(Example_Initial, 5, 5, 1)]
        public void CalculateLitNeighborsForLight(string gridLayout, int xIndex, int yIndex, int expectedLitNeighbors)
        {
            var board = new Board(6);
            board.SetBoardValues(gridLayout);
            var actual = board.CalculateLitNeighborsForLight(xIndex, yIndex);
            Assert.AreEqual(expectedLitNeighbors, actual);
        }
    }
}
