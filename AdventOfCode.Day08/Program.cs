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

            var encodedCharCount = entries
                .Select(x => Processor.GetEncodedLiterialCharacterCount(x))
                .Sum();

            var part2Answer = encodedCharCount - literalCharCount;
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
            input = Regex.Replace(input, @"(\\(?:[^x]|x..))", "_"); //get rid of hex values and slash-something's
            input = input.Substring(1, input.Length - 2); //get rid of the first and last char (double quotes)

            return input.Length;
        }

        public static int GetEncodedLiterialCharacterCount(string input)
        {
            input = Regex.Replace(input, @"\\|""", "__"); //replace a slash and a double quote with 2 characters
            input = "-" + input + "-"; //wrap in "quotes"

            return input.Length;            
        }
    }
}
