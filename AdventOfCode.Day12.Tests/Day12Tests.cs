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


        [TestCase("[1,2,3]", 6)]
        [TestCase("[1,{\"c\":\"red\",\"b\":2},3]", 4)]
        [TestCase("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0)]
        [TestCase("[1,\"red\",5]", 6)]
        public void GetSumOfAllNonRedNumbers(string inputJson, int expectedNumberSum)
        {
            var actualSum = Processor.GetSumOfAllNonRedNumbers(inputJson);
            Assert.AreEqual(expectedNumberSum, actualSum);
        }
    }
}
