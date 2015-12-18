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
            var startingBoard = new Board(initialLayout);
            var actual = Processor.RunBoardAnimation(startingBoard, stepCount);

            Assert.AreEqual(expectedOutput, actual.ToString());
        }
    }
}
