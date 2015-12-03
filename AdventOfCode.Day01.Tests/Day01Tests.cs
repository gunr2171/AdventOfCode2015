using System;
using NUnit.Framework;

namespace AdventOfCode.Day01.Tests
{
    [TestFixture]
    public class Day01Tests
    {
        [TestCase("(())", 0)]
        [TestCase("()()", 0)]
        [TestCase("(((", 3)]
        [TestCase("(()(()(", 3)]
        [TestCase("))(((((", 3)]
        [TestCase("())", -1)]
        [TestCase("))(", -1)]
        [TestCase(")))", -3)]
        [TestCase(")())())", -3)]
        public void GetFinalFloorNumber(string instructions, int expectedFinalFloorNumber)
        {
            var result = Processor.GetFinalFloorNumber(instructions);
            Assert.AreEqual(expectedFinalFloorNumber, result);
        }

        [TestCase(")", 1)]
        [TestCase("()())", 5)]
        public void GetInstructionPositionThatLeadToBasement(string instructions, int expectedPosition)
        {
            var result = Processor.GetInstructionPositionThatLeadToBasement(instructions);
            Assert.AreEqual(expectedPosition, result);
        }
    }
}
