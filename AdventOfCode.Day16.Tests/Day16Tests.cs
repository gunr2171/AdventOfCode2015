using System;
using NUnit.Framework;

namespace AdventOfCode.Day16.Tests
{
    [TestFixture]
    public class Day16Tests
    {
        [TestCase(1, 1, true)]
        [TestCase(1, 2, false)]
        [TestCase(null, 4, true)]
        public void TestValueEqualityOrNull(int? valueToTest, int testingValue, bool expectedResult)
        {
            var actual = Processor.TestValueEqualityOrNull(valueToTest, testingValue);
            Assert.AreEqual(expectedResult, actual);
        }

        [TestCase(1, 1, false)]
        [TestCase(2, 1, true)]
        [TestCase(null, 4, true)]
        public void TestValueGreaterThanOrNull(int? valueToTest, int testingValue, bool expectedResult)
        {
            var actual = Processor.TestValueGreaterThanOrNull(valueToTest, testingValue);
            Assert.AreEqual(expectedResult, actual);
        }

        [TestCase(1, 1, false)]
        [TestCase(1, 2, true)]
        [TestCase(null, 4, true)]
        public void TestValueLessThanOrNull(int? valueToTest, int testingValue, bool expectedResult)
        {
            var actual = Processor.TestValueLessThanOrNull(valueToTest, testingValue);
            Assert.AreEqual(expectedResult, actual);
        }
    }
}
