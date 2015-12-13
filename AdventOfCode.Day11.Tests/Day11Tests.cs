using System;
using NUnit.Framework;

namespace AdventOfCode.Day11.Tests
{
    [TestFixture]
    public class Day11Tests
    {
        [TestCase("hijklmmn", true)]
        [TestCase("abbceffg", false)]
        [TestCase("abc", true)]
        public void PassesRule1(string input, bool expectedPass)
        {
            var actual = Processor.PassesRule1(input);
            Assert.AreEqual(expectedPass, actual);
        }

        [TestCase("hijklmmn", false)]
        [TestCase("abc", true)]
        [TestCase("abi", false)]
        public void PassesRule2(string input, bool expectedPass)
        {
            var actual = Processor.PassesRule2(input);
            Assert.AreEqual(expectedPass, actual);
        }

        [TestCase("abbceffg", true)]
        [TestCase("abbcegjk", false)]
        [TestCase("aabb", true)]
        [TestCase("aaa", false)]
        [TestCase("aaaa", true)]
        public void PassesRule3(string input, bool expectedPass)
        {
            var actual = Processor.PassesRule3(input);
            Assert.AreEqual(expectedPass, actual);
        }

        [TestCase("abcdefgh", "abcdffaa")]
        [TestCase("ghijklmn", "ghjaabcc")]
        public void GetNextPassword(string input, string expectedNextPassword)
        {
            var actual = Processor.GetNextPassword(input);
            Assert.AreEqual(expectedNextPassword, actual);
        }

        [TestCase("aaab", "aaac")]
        [TestCase("azz", "baa")]
        [TestCase("ahz", "aja")]
        public void IncrementPassword(string startingPassword, string expectedNextPassword)
        {
            var actual = Processor.IncrementPassword(startingPassword);
            Assert.AreEqual(expectedNextPassword, actual);
        }

        //[TestCase("abc", "abd", true)]
        //[TestCase("abcd", "afad", true)]
        //[TestCase("abcd", "aabc", false)]
        //public void IsNewPasswordAfterStartingPassword(string oldPassword, string newPassword, bool expectedPass)
        //{
        //    var actual = Processor.IsNewPasswordAfterStartingPassword(newPassword, oldPassword);
        //    Assert.AreEqual(expectedPass, actual);
        //}
    }
}
