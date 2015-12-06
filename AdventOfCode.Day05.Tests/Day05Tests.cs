using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
        public void IsNiceStringV1(string input, bool expectedNiceValue)
        {
            var result = Processor.IsNiceStringV1(input);
            Assert.AreEqual(expectedNiceValue, result);
        }

        [TestCase("qjhvhtzxzqqjkmpb", true)]
        [TestCase("xxyxx", true)]
        [TestCase("uurcxstgmygtbstg", false)]
        [TestCase("ieodomkazucvgmuy", false)]
        [TestCase("bfbaaa", false)]
        [TestCase("xilodxfuxphuiiii", true)]
        [TestCase("tdfvkreormspprer", true)]
        [TestCase("dwmxqudvxqdenrur", true)]
        public void IsNiceStringV2(string input, bool expectedNiceValue)
        {
            var result = Processor.IsNiceStringV2(input);
            Assert.AreEqual(expectedNiceValue, result);
        }

        [TestCase("xyx", true)]
        [TestCase("abcdefeghi", true)]
        [TestCase("aaa", true)]
        [TestCase("aaab", true)]
        [TestCase("uurcxstgmygtbstg", false)]
        [TestCase("ieodomkazucvgmuy", true)]
        [TestCase("sszojmmrrkwuftyv", false)]
        [TestCase("abcdefe", true)]
        public void DetermineIfInputHasValid3LetterCombo(string input, bool expectedResult)
        {
            var result = Processor.DetermineIfInputHasValid3LetterCombo(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("xyxy", true)]
        [TestCase("aabcdefgaa", true)]
        [TestCase("aaa", false)]
        [TestCase("uurcxstgmygtbstg", true)]
        [TestCase("ieodomkazucvgmuy", false)]
        [TestCase("sszojmmrrkwuftyv", false)]
        public void DetermineIfInputHasValidRepeatingLetterPair(string input, bool expectedResult)
        {
            var result = Processor.DetermineIfInputHasValidRepeatingLetterPair(input);
            Assert.AreEqual(expectedResult, result);
        }

        //[TestCase("ab", "bc", -1)]
        //public void LetterPairCompare(string s1, string s2, int expectedCompare)
        //{
        //    var pair1 = new Processor.LetterPair(s1[0], s1[1], 0);
        //    var pair2 = new Processor.LetterPair(s2[0], s2[1], 0);

        //    var result = pair1.CompareTo(pair2);

        //    Assert.AreEqual(expectedCompare, result);
        //}

        //[Test]
        //public void TestComboGenerator()
        //{
        //    var sourceList = new List<Processor.LetterPair>();
        //    sourceList.Add(new Processor.LetterPair('a', 'b', 0));
        //    sourceList.Add(new Processor.LetterPair('a', 'b', 8));
        //    //sourceList.Add(new Processor.LetterPair('a', 'b', 12));

        //    var compare = sourceList[0].CompareTo(sourceList[1]);

        //    var combinations = sourceList.GetKCombs(2).ToList();

        //    Assert.AreEqual(1, combinations.Count);
        //}
    }
}