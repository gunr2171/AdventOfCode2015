using System;
using NUnit.Framework;

namespace AdventOfCode.Day03.Tests
{
    [TestFixture]
    public class Day03Tests
    {
        [TestCase(">", 1, 2)]
        [TestCase("^>v<", 1, 4)]
        [TestCase("^v^v^v^v^v", 1, 2)]
        [TestCase("^v", 2, 3)]
        [TestCase("^>v<", 2, 3)]
        [TestCase("^v^v^v^v^v", 2, 11)]
        public void GetUniqueHouseCount(string instructions, int deliverers, int expectedHouseCount)
        {
            var result = Processor.GetUniqueHouseCount(instructions, deliverers);
            Assert.AreEqual(expectedHouseCount, result);
        }
    }
}
