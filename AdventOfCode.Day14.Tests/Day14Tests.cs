using System;
using NUnit.Framework;

namespace AdventOfCode.Day14.Tests
{
    [TestFixture]
    public class Day14Tests
    {
        Race race;

        [OneTimeSetUp]
        public void SetupExample()
        {
            var racers = new[]
            {
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."
            };

            race = new Race();

            foreach (var racer in racers)
            {
                race.AddReindeer(racer);
            }
        }

        [TestCase(1, 16)]
        [TestCase(10, 160)]
        [TestCase(11, 176)]
        [TestCase(12, 176)]
        [TestCase(1000, 1120)]
        public void CalculateDistanceOfFurthestReindeerAtMoment(int secondsElapsed, int expectedFurthestDistance)
        {
            var actual = race.CalculateDistanceOfFurthestReindeerAtMoment(secondsElapsed);
            Assert.AreEqual(expectedFurthestDistance, actual);
        }

        [TestCase(1, 1)]
        [TestCase(140, 139)]
        [TestCase(1000, 689)]
        public void CalculateMostPointsAtMoment(int secondsElapsed, int expectedFurthestDistance)
        {
            var actual = race.CalculateMostPointsAtMoment(secondsElapsed);
            Assert.AreEqual(expectedFurthestDistance, actual);
        }
    }
}
