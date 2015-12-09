using System;
using NUnit.Framework;

namespace AdventOfCode.Day09.Tests
{
    [TestFixture]
    public class Day09Tests
    {
        [Test]
        public void Part1Example()
        {
            var calculator = new Calculator();

            calculator.AddDistanceRecord("London to Dublin = 464");
            calculator.AddDistanceRecord("London to Belfast = 518");
            calculator.AddDistanceRecord("Dublin to Belfast = 141");

            var actual = calculator.CalculateShortestRoute();

            Assert.AreEqual(605, actual);
        }
    }
}
