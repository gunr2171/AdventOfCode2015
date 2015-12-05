using System;
using NUnit.Framework;

namespace AdventOfCode.Day05.Tests
{
    [TestFixture]
    public class Day05Tests
    {
        [TestCase("ugknbfddgicrmopn", true)]
        [TestCase("aaa", true)]
        [TestCase("jchzalrnumimnmhp", false)]
        [TestCase("haegwjzuvuyypxyu", false)]
        [TestCase("dvszwmarrgswjxmb", false)]
        public void IsNiceString(string input, bool expectedNiceValue)
        {
            var result = Processor.IsNiceString(input);
            Assert.AreEqual(expectedNiceValue, result);
        }
    }
}
