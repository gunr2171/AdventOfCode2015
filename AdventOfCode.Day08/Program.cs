using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var entries = File.ReadAllLines("Input.txt").ToList();

            var literalCharCount = entries
                .Select(x => Processor.GetLiteralCharacterCount(x))
                .Sum();

            var memoryCharCount = entries
                .Select(x => Processor.GetMemoryCharacterCount(x))
                .Sum();

            var part1Answer = literalCharCount - memoryCharCount;
        }
    }

    public static class Processor
    {
        public static int GetLiteralCharacterCount(string input)
        {
            return input.Length;
        }

        public static int GetMemoryCharacterCount(string input)
        {
            input = Regex.Replace(input, @"(\\x\w\w)", "_"); //get rid of hex values
            input = Regex.Replace(input, @"(\\.)", "_"); //get rid of a slash-something.
            input = input.Substring(1, input.Length - 2); //get rid of the first and last char (double quotes)

            return input.Length;
        }

    }
}
