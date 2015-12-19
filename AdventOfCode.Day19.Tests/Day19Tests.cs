using System;
using NUnit.Framework;

namespace AdventOfCode.Day19.Tests
{
    [TestFixture]
    public class Day19Tests
    {
        [TestCase("HOH", 4)]
        [TestCase("HOHOHO", 7)]
        public void Part1Example(string input, int expectedDistinctMolecules)
        {
            var machine = new Machine();

            machine.AddReplacement("H => HO");
            machine.AddReplacement("H => OH");
            machine.AddReplacement("O => HH");

            var actual = machine.CalculateDistinctMoleculesCount(input);
            Assert.AreEqual(expectedDistinctMolecules, actual);
        }
    }
}
