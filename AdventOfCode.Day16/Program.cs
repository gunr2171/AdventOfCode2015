using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;

namespace AdventOfCode.Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines("Input.txt");

            var auntSues = inputLines
                .Select(x => new AuntSue(x))
                .ToList();

            var part1MatchingSues = auntSues
                .Where(x => Processor.TestValueEqualityOrNull(x["children"], 3))
                .Where(x => Processor.TestValueEqualityOrNull(x["cats"], 7))
                .Where(x => Processor.TestValueEqualityOrNull(x["samoyeds"], 2))
                .Where(x => Processor.TestValueEqualityOrNull(x["pomeranians"], 3))
                .Where(x => Processor.TestValueEqualityOrNull(x["akitas"], 0))
                .Where(x => Processor.TestValueEqualityOrNull(x["vizslas"], 0))
                .Where(x => Processor.TestValueEqualityOrNull(x["goldfish"], 5))
                .Where(x => Processor.TestValueEqualityOrNull(x["trees"], 3))
                .Where(x => Processor.TestValueEqualityOrNull(x["cars"], 2))
                .Where(x => Processor.TestValueEqualityOrNull(x["perfumes"], 1))
                .ToList();

            var part1Answer = part1MatchingSues.Single().SueNumber;

            var part2MatchingSues = auntSues
                .Where(x => Processor.TestValueEqualityOrNull(x["children"], 3))
                .Where(x => Processor.TestValueGreaterThanOrNull(x["cats"], 7))
                .Where(x => Processor.TestValueEqualityOrNull(x["samoyeds"], 2))
                .Where(x => Processor.TestValueLessThanOrNull(x["pomeranians"], 3))
                .Where(x => Processor.TestValueEqualityOrNull(x["akitas"], 0))
                .Where(x => Processor.TestValueEqualityOrNull(x["vizslas"], 0))
                .Where(x => Processor.TestValueLessThanOrNull(x["goldfish"], 5))
                .Where(x => Processor.TestValueGreaterThanOrNull(x["trees"], 3))
                .Where(x => Processor.TestValueEqualityOrNull(x["cars"], 2))
                .Where(x => Processor.TestValueEqualityOrNull(x["perfumes"], 1))
                .ToList();

            var part2Answer = part2MatchingSues.Single().SueNumber;
        }
    }

    public static class Processor
    {
        public static bool TestValueEqualityOrNull(int? valueToTest, int testingValue)
        {
            return valueToTest == null || valueToTest == testingValue;
        }

        public static bool TestValueGreaterThanOrNull(int? valueToTest, int testingValue)
        {
            return valueToTest == null || valueToTest > testingValue;
        }

        public static bool TestValueLessThanOrNull(int? valueToTest, int testingValue)
        {
            return valueToTest == null || valueToTest < testingValue;
        }
    }

    public class AuntSue
    {
        public int SueNumber { get; set; }

        private Dictionary<string, int> entries = new Dictionary<string, int>();

        public int? this[string entryKey]
        {
            get
            {
                if (!entries.ContainsKey(entryKey))
                    return null;

                return entries[entryKey];
            }
            set
            {
                if (value == null)
                    return;

                if (entries.ContainsKey(entryKey))
                    entries[entryKey] = value.Value;
                else
                    entries.Add(entryKey, value.Value);
            }
        }

        public AuntSue(string instruction)
        {
            var match = Regex.Match(instruction, @"^Sue (\d+):(.+)$");
            SueNumber = match.Groups[1].Value.Parse<int>();

            var entries = match.Groups[2].Value
                .Split(',')
                .Select(x => x.Trim())
                .Select(x => x.Split(':').Select(y => y.Trim()))
                .ToDictionary(x => x.First(), x => x.Last().Parse<int>());

            foreach (var entry in entries)
            {
                this[entry.Key] = entry.Value;
            }
        }
    }
}
