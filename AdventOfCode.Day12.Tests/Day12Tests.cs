using System;
using NUnit.Framework;

namespace AdventOfCode.Day12.Tests
{
    [TestFixture]
    public class Day12Tests
    {
        [TestCase("[1,2,3]", 6)]
        [TestCase("{\"a\":2,\"b\":4}", 6)]
        [TestCase("[[[3]]]", 3)]
        [TestCase("{\"a\":{\"b\":4},\"c\":-1}", 3)]
        [TestCase("{\"a\":[-1,1]}", 0)]
        [TestCase("[-1,{\"a\":1}]", 0)]
        [TestCase("[]", 0)]
        [TestCase("{}", 0)]
        public void GetSumOfAllNumberInJson(string inputJson, int expectedNumberSum)
        {
            var actualSum = Processor.GetSumOfAllNumbersInJson(inputJson);
            Assert.AreEqual(expectedNumberSum, actualSum);
        }
    }
}
