using System;
using NUnit.Framework;

namespace AdventOfCode.Day04.Tests
{
    [TestFixture]
    public class Day04Tests
    {
        [TestCase("abcdef", 609043)]
        [TestCase("pqrstuv", 1048970)]
        public void GetLowestConcatNumberForSeed(string seed, int expectedNumber)
        {
            var result = Processor.GetLowestConcatNumberForSeed(seed);
            Assert.AreEqual(expectedNumber, result);
        }
    }
}
