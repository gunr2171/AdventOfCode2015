using System;
using NUnit.Framework;

namespace AdventOfCode.Day10.Tests
{
    [TestFixture]
    public class Day10Tests
    {
        [TestCase("1", "11")]
        [TestCase("11", "21")]
        [TestCase("21", "1211")]
        [TestCase("1211", "111221")]
        [TestCase("111221", "312211")]
        public void GetNextLookAndSayValue(string intput, string expectedNextValue)
        {
            var actual = Processor.GetNextLookAndSayValue(intput);
            Assert.AreEqual(expectedNextValue, actual);
        }
    }
}
