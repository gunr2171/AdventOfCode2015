using System;
using NUnit.Framework;

namespace AdventOfCode.Day06.Tests
{
    [TestFixture]
    public class Day06Tests
    {
        [TestCase("turn on 0,0 through 999,999", 1000000)]
        [TestCase("toggle 0,0 through 999,0", 1000)]
        [TestCase("turn off 499,499 through 500,500", 0)]
        public void ApplyInstruction(string instruction, int expectedLightsOn)
        {
            var board = new Board();
            board.ApplyInstruction(instruction);
            var result = board.LightsTurnedOn();
            Assert.AreEqual(expectedLightsOn, result);
        }
    }
}
