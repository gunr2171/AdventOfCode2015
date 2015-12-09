using System;
using NUnit.Framework;

namespace AdventOfCode.Day09.Tests
{
    [TestFixture]
    public class Day09Tests
    {
        private Calculator part1Calculator;

        [OneTimeSetUp]
        public void Part1Setup()
        {
            part1Calculator = new Calculator();

            part1Calculator.AddDistanceRecord("London to Dublin = 464");
            part1Calculator.AddDistanceRecord("London to Belfast = 518");
            part1Calculator.AddDistanceRecord("Dublin to Belfast = 141");
        }

        [TestCase(982, "Dublin", "London", "Belfast")]
        [TestCase(605, "London", "Dublin", "Belfast")]
        [TestCase(659, "London", "Belfast", "Dublin")]
        [TestCase(659, "Dublin", "Belfast", "London")]
        [TestCase(605, "Belfast", "Dublin", "London")]
        [TestCase(982, "Belfast", "London", "Dublin")]
        public void GenerateDistanceFromCityList(int expectedDistance, params string[] cities)
        {
            var actual = part1Calculator.GenerateDistanceFromCityList(cities);
            Assert.AreEqual(expectedDistance, actual);
        }

        [Test]
        public void CalculateShortestRoute()
        {
            var actual = part1Calculator.CalculateShortestRoute();
            Assert.AreEqual(605, actual);
        }

        [Test]
        public void CalculateLongestRoute()
        {
            var actual = part1Calculator.CalculateLongestRoute();
            Assert.AreEqual(982, actual);
        }
    }
}
