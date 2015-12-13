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
            var input = "cqjxjnds";

            var part1Answer = Processor.GetNextPassword(input);
            var part2Answer = Processor.GetNextPassword(part1Answer);
        }
    }

    public static class Processor
    {
        public static IEnumerable<string> EnumPossiblePasswords(string startingPassword)
        {
            var currentPassword = startingPassword;

            while (currentPassword != "zzzzzzzz")
            {
                currentPassword = IncrementPassword(currentPassword);
                yield return currentPassword;
            }
        }

        public static string IncrementPassword(string inputPassword)
        {
            var incrementedPassword = IncrementPassword(inputPassword.ToCharArray(), inputPassword.Length - 1);
            return incrementedPassword;
        }

        private static string IncrementPassword(char[] chars, int indexToIncrement)
        {
            if (chars[indexToIncrement] == 'z') //if the char you are tring to increase is at the end
            {
                chars[indexToIncrement] = 'a'; //set it back to the start 

                if (indexToIncrement > 0) //and assuming you are not at the far left side of the string
                    chars = IncrementPassword(chars, indexToIncrement - 1).ToCharArray(); //increase the char to the left of this one
            }
            else //just increment this char as normal
            {
                char newChar = ++chars[indexToIncrement];

                if (newChar == 'i' || newChar == 'l' || newChar == 'o')
                    newChar++; //skip it

                chars[indexToIncrement] = newChar;
            }

            var result = chars.ToCSV("");
            return result;
        }

        public static string GetNextPassword(string input)
        {
            var possiblePasswords = EnumPossiblePasswords(input);

            var nextPassword = possiblePasswords
                .Where(x => PassesRule1(x))
                .Where(x => PassesRule2(x))
                .Where(x => PassesRule3(x))
                .First();

            return nextPassword;
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
