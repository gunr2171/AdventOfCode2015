using System;
using NUnit.Framework;

namespace AdventOfCode.Day04.Tests
{
    [TestFixture]
    public class Day04Tests
    {
        [TestCase("abcdef", 5, 609043)]
        [TestCase("pqrstuv", 5, 1048970)]
        public void GetLowestConcatNumberForSeed(string seed, int hashLeadingZeros, int expectedNumber)
        {
            var result = Processor.GetLowestConcatNumberForSeed(seed, hashLeadingZeros);
            Assert.AreEqual(expectedNumber, result);
        }
    }
}
