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

            var matchingSues = auntSues
                .Where(x => x.Children == null || x.Children == 3)
                .Where(x => x.Cats == null || x.Cats == 7)
                .Where(x => x.Samoyeds == null || x.Samoyeds == 2)
                .Where(x => x.Pomeranians == null || x.Pomeranians == 3)
                .Where(x => x.Akitas == null || x.Akitas == 0)
                .Where(x => x.Vizslas == null || x.Vizslas == 0)
                .Where(x => x.Goldfish == null || x.Goldfish == 5)
                .Where(x => x.Trees == null || x.Trees == 3)
                .Where(x => x.Cars == null || x.Cars == 2)
                .Where(x => x.Perfumes == null || x.Perfumes == 1)
                .ToList();

            var part1Answer = matchingSues.Single().SueNumber;
        }
    }

    public class AuntSue
    {
        public int SueNumber { get; set; }

        public int? Children { get; set; }
        public int? Cats { get; set; }
        public int? Samoyeds { get; set; }
        public int? Pomeranians { get; set; }
        public int? Akitas { get; set; }
        public int? Vizslas { get; set; }
        public int? Goldfish { get; set; }
        public int? Trees { get; set; }
        public int? Cars { get; set; }
        public int? Perfumes { get; set; }

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
                switch (entry.Key)
                {
                    case "children": Children = entry.Value; break;
                    case "cats": Cats = entry.Value; break;
                    case "samoyeds": Samoyeds = entry.Value; break;
                    case "pomeranians": Pomeranians = entry.Value; break;
                    case "akitas": Akitas = entry.Value; break;
                    case "vizslas": Vizslas = entry.Value; break;
                    case "goldfish": Goldfish = entry.Value; break;
                    case "trees": Trees = entry.Value; break;
                    case "cars": Cars = entry.Value; break;
                    case "perfumes": Perfumes = entry.Value; break;
                }
            }
        }
    }
}
