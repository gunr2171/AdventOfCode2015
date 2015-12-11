using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;

namespace AdventOfCode.Day11
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public static class Processor
    {
        private static List<char> passwordLetters;

        static Processor() //yeah static constructors, because it makes sense!
        {
            passwordLetters = Enumerable.Range(97, 26)
                .Select(x => (char)x)
                .OrderBy(x => x)
                .ToList();
        }

        public static string GetNextPassword(string input)
        {
            throw new NotImplementedException();
        }

        public static bool PassesRule1(string input)
        {
            //not that I expect this to happen, but just in case
            if (input.Length < 3)
                return false;

            var incrementingLetterCount = 1;

            for (int i = 1; i < input.Length; i++)
            {
                var delta = input[i] - input[i - 1];

                if (delta == 1)
                {
                    incrementingLetterCount++;

                    //have we reached a full set?
                    if (incrementingLetterCount == 3)
                    {
                        return true;
                    }
                }
                else
                {
                    //reset
                    incrementingLetterCount = 1;
                }
            }

            //never hit 3
            return false;
        }

        public static bool PassesRule2(string input)
        {
            var invalidLetters = new[] { 'i', 'o', 'l' };

            var containsInvalidLetter = input
                .Where(x => x.In(invalidLetters))
                .Any();

            return !containsInvalidLetter;
        }

        public static bool PassesRule3(string input)
        {
            var isMatch = Regex.IsMatch(input, @"(.)\1.*(.)\2");
            return isMatch;
        }
    }
}
