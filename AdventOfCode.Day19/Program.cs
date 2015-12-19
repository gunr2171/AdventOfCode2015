using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day19
{
    class Program
    {
        static void Main(string[] args)
        {

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
            var startingMolecule = new Molecule(inputMolecule);

            foreach (var atom in startingMolecule.EnumAtoms())
            {
                var matchingReplacements = replacements
                    .Where(x => x.Input == atom);

                foreach (var replacement in matchingReplacements)
                {
                    
                }
            }
        }
    }

    public class Replacement
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }

    public class Molecule
    {
        private List<char> letters = new List<char>();

        public Molecule(string input)
        {

        }

        public IEnumerable<string> EnumAtoms()
        {

        }
    }

}
