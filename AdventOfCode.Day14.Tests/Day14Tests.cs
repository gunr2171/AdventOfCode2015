using System;
using NUnit.Framework;

namespace AdventOfCode.Day14.Tests
{
    [TestFixture]
    public class Day14Tests
    {
        Race part1race;

        [OneTimeSetUp]
        public void SetupPart1Example()
        {
            var racers = new[]
            {
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."
            };

            part1race = new Race();

            foreach (var racer in racers)
            {
                part1race.AddReindeer(racer);
            }
        }

        [TestCase(1, 16)]
        [TestCase(10, 160)]
        [TestCase(11, 176)]
        [TestCase(12, 176)]
        [TestCase(1000, 1120)]
        public void Part1Example(int secondsElapsed, int expectedFurthestDistance)
        {
            var actual = part1race.CalculateDistanceOfFurthestReindeerAtMoment(secondsElapsed);
            Assert.AreEqual(expectedFurthestDistance, actual);
        }
    }
}
