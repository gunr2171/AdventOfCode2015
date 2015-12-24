using System;
using NUnit.Framework;

namespace AdventOfCode.Day20.Tests
{
    [TestFixture]
    public class Day20Tests
    {
        [TestCase(1, 10)]
        [TestCase(2, 30)]
        [TestCase(3, 40)]
        [TestCase(4, 70)]
        [TestCase(5, 60)]
        [TestCase(6, 120)]
        [TestCase(7, 80)]
        [TestCase(8, 150)]
        [TestCase(9, 130)]
        public void GetPresentCountForHouseNumber(int houseNumber, int expectedPresentCount)
        {
            var actual = Processor.GetPresentCountForHouseNumber(houseNumber);
            Assert.AreEqual(expectedPresentCount, actual);
        }
    }
}
