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

        //---------------------------------------------------------------------

        private const string Example2_Initial = @"##.#.#
...##.
#....#
..#...
#.#..#
####.#";

        private const string Example2_1 = @"#.##.#
####.#
...##.
......
#...#.
#.####";

        private const string Example2_2 = @"#..#.#
#....#
.#.##.
...##.
.#..##
##.###";

        private const string Example2_3 = @"#...##
####.#
..##.#
......
##....
####.#";

        private const string Example2_4 = @"#.####
#....#
...#..
.##...
#.....
#.#..#";

        private const string Example2_5 = @"##.###
.##..#
.##...
.##...
#.#...
##...#";


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

        [TestCase(Example2_Initial, 1, Example2_1)]
        [TestCase(Example2_Initial, 2, Example2_2)]
        [TestCase(Example2_Initial, 3, Example2_3)]
        [TestCase(Example2_Initial, 4, Example2_4)]
        [TestCase(Example2_Initial, 5, Example2_5)]
        public void RunBoardAnimation_Stuck(string initialLayout, int stepCount, string expectedOutput)
        {
            var startingBoard = new StuckBoard(6);
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
            var board = new Board(1);
            var actual = board.CalculateNewValueForLight(currentValue, litNeighborCount, 0, 0);
            Assert.AreEqual(expectedNewValue, actual);
        }

        [TestCase(true, 2, 3, 3, true)]
        [TestCase(true, 3, 3, 3, true)]
        [TestCase(true, 4, 3, 3, false)]
        [TestCase(true, 1, 3, 3, false)]
        [TestCase(false, 3, 3, 3, true)]
        [TestCase(false, 1, 3, 3, false)]
        [TestCase(false, 4, 0, 0, true)]
        [TestCase(true, 4, 0, 5, true)]
        public void CalculateNewValueForLight_Stuck(bool currentValue, int litNeighborCount, int x, int y, bool expectedNewValue)
        {
            var board = new StuckBoard(6);
            var actual = board.CalculateNewValueForLight(currentValue, litNeighborCount, x, y);
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

        [TestCase(Example_Initial, 15)]
        [TestCase(Example_1, 11)]
        [TestCase(Example_4, 4)]
        [TestCase(Example2_5, 17)]
        public void GetCountOfLitLights(string boardLayout, int expectedLitLights)
        {
            var board = new Board(6);
            board.SetBoardValues(boardLayout);

            var actual = Processor.GetCountOfLitLights(board);
            Assert.AreEqual(expectedLitLights, actual);
        }
    }
}
