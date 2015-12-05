using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var part1Answer = File.ReadAllLines("Input.txt")
                .Where(x => Processor.IsNiceString(x))
                .Count();
        }
    }

    public static class Processor
    {
        static char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
        static string[] badPhrases = new string[] { "ab", "cd", "pq", "xy" };

        public static bool IsNiceString(string input)
        {
            var vowelCount = (from inputChar in input
                              join vowelChar in vowels on inputChar equals vowelChar
                              select inputChar)
                              .Count();

            if (vowelCount < 3)
                return false;

            var hasRepeatingCharacter = input
                .Skip(1)
                .Where((x, i) => x == input[i])
                .Any();

            if (!hasRepeatingCharacter)
                return false;

            var firstFoundBadPhrase = badPhrases
                .Where(x => input.Contains(x))
                .FirstOrDefault();

            if (firstFoundBadPhrase != null)
                return false;

            //didn't catch on any of the above rules, it's good
            return true;
        }
    }
}
