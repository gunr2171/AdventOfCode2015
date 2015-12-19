using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;

namespace AdventOfCode.Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines("Input.txt");

            var replacementLines = inputLines
                .TakeWhile(x => !x.IsNullOrWhiteSpace());

            var startingMolecule = inputLines.Last();

            var machine = new Machine();

            foreach (var replacement in replacementLines)
            {
                machine.AddReplacement(replacement);
            }

            var part1Answer = machine.CalculateDistinctMoleculesCount(startingMolecule);
        }
    }

    public class Machine
    {
        private List<Replacement> replacements = new List<Replacement>();

        public void AddReplacement(string rawInstruction)
        {
            var match = Regex.Match(rawInstruction, @"(\w+) => (\w+)");

            var newReplacement = new Replacement();
            newReplacement.Input = match.Groups[1].Value;
            newReplacement.Output = match.Groups[2].Value;

            replacements.Add(newReplacement);
        }

        public int CalculateDistinctMoleculesCount(string inputMolecule)
        {
            List<string> allMolecules = new List<string>();

            foreach (var replacement in replacements)
            {
                var regex = new Regex(replacement.Input);
                var matches = regex.Matches(inputMolecule);

                foreach (var match in matches.OfType<Match>())
                {
                    var replacementValue = replacement.Output;

                    var originalLeft = inputMolecule.Substring(0, match.Index);
                    var originalRight = inputMolecule.Substring(match.Index + match.Length);

                    var newString = originalLeft + replacementValue + originalRight;

                    allMolecules.Add(newString);
                }
            }

            var distinctCount = allMolecules.Distinct().Count();
            return distinctCount;
        }
    }

    public class Replacement
    {
        public string Input { get; set; }
        public string Output { get; set; }

        public override string ToString()
        {
            return "{0} => {1}".FormatInline(Input, Output);
        }
    }
}
