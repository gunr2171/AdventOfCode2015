using System;
using NUnit.Framework;

namespace AdventOfCode.Day07.Tests
{
    [TestFixture]
    public class Day07Tests
    {
        private CircuitBoard part1Board;
        private CircuitBoard part1BoardJubmled;

        [OneTimeSetUp]
        public void CreatePart1ExampleCircuitBoard()
        {
            var instructions = new[]
            {
                "123 -> x",
                "456 -> y",
                "x AND y -> d",
                "x OR y -> e",
                "x LSHIFT 2 -> f",
                "y RSHIFT 2 -> g",
                "NOT x -> h",
                "NOT y -> i",
            };

            part1Board = new CircuitBoard();

            foreach (var instruction in instructions)
            {
                part1Board.ApplyInstruction(instruction);
            }

            part1Board.CalculateWireValues();
        }

        [OneTimeSetUp]
        public void CreatePart1ExampleJumbledCircuitBoard()
        {
            var instructions = new[]
            {
                "NOT y -> i",
                "x OR y -> e",
                "x AND y -> d",
                "456 -> y",
                "y RSHIFT 2 -> g",
                "NOT x -> h",
                "x LSHIFT 2 -> f",
                "123 -> x",
            };

            part1BoardJubmled = new CircuitBoard();

            foreach (var instruction in instructions)
            {
                part1BoardJubmled.ApplyInstruction(instruction);
            }

            part1BoardJubmled.CalculateWireValues();
        }

        [TestCase("d", 72)]
        [TestCase("e", 507)]
        [TestCase("f", 492)]
        [TestCase("g", 114)]
        [TestCase("h", 65412)]
        [TestCase("i", 65079)]
        [TestCase("x", 123)]
        [TestCase("y", 456)]
        public void Part1TestWireSignalValue(string wireName, int expectedValue)
        {
            var actual = part1Board.GetWireSignal(wireName);
            Assert.AreEqual(expectedValue, actual);
        }

        [TestCase("d", 72)]
        [TestCase("e", 507)]
        [TestCase("f", 492)]
        [TestCase("g", 114)]
        [TestCase("h", 65412)]
        [TestCase("i", 65079)]
        [TestCase("x", 123)]
        [TestCase("y", 456)]
        public void Part1TestWireSignalValueJumbled(string wireName, int expectedValue)
        {
            var actual = part1BoardJubmled.GetWireSignal(wireName);
            Assert.AreEqual(expectedValue, actual);
        }
    }
}
