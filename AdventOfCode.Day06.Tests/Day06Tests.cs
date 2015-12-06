using System;
using NUnit.Framework;

namespace AdventOfCode.Day06.Tests
{
    [TestFixture]
    public class Day06Tests
    {
        private const int TOTAL_LIGHTS = 1000000;

        [TestCase("turn on 0,0 through 999,999", TOTAL_LIGHTS)]
        [TestCase("toggle 0,0 through 999,0", 1000)]
        public void OnOffBoard_ApplySingleInstruction(string instruction, int expectedLightsOn)
        {
            var board = new OnOffBoard();
            board.ApplyInstruction(instruction);
            var result = board.LightsTurnedOn();
            Assert.AreEqual(expectedLightsOn, result);
        }

        [TestCase("turn off 499,499 through 500,500", TOTAL_LIGHTS - 4)]
        public void OnOffBoard_ApplySingleInstruction_StartWithFullOn(string instruction, int expectedLightsOn)
        {
            var board = new OnOffBoard();
            board.ApplyInstruction("turn on 0,0 through 999,999");
            board.ApplyInstruction(instruction);
            var result = board.LightsTurnedOn();

            Assert.AreEqual(expectedLightsOn, result);
        }

        [TestCase("turn on 0,0 through 0,0", 1)]
        [TestCase("toggle 0,0 through 999,999", 2000000)]
        public void BrightnessBoard_ApplySingleInstruction(string instruction, int expectedTotalBrightness)
        {
            var board = new BrightnessBoard();
            board.ApplyInstruction(instruction);
            var result = board.TotalBrightness();
            Assert.AreEqual(expectedTotalBrightness, result);
        }
    }
}
