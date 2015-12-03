using System;
using NUnit.Framework;

namespace AdventOfCode.Day02.Tests
{
    [TestFixture]
    public class Day02Tests
    {
        [TestCase(2, 3, 4, 58)]
        [TestCase(1, 1, 10, 43)]
        public void CalculateRequiredWrappingPaper(int length, int width, int height, int expectedTotal)
        {
            var gift = new Gift(length, width, height);
            Assert.AreEqual(expectedTotal, gift.CalculateRequiredWrappingPaper());
        }

        [TestCase(2, 3, 4, 34)]
        [TestCase(1, 1, 10, 14)]
        public void CalculateRequiredRibbon(int length, int width, int height, int expectedTotal)
        {
            var gift = new Gift(length, width, height);
            Assert.AreEqual(expectedTotal, gift.CalculateRequiredRibbon());
        }
    }
}
