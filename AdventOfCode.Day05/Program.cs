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
                .Where(x => Processor.IsNiceStringV1(x))
                .Count();

            var part2Answer = File.ReadAllLines("Input.txt")
                .Where(x => Processor.IsNiceStringV2(x))
                .Count();
        }
    }

    public static class Processor
    {
        static char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
        static string[] badPhrases = new string[] { "ab", "cd", "pq", "xy" };

        public static bool IsNiceStringV1(string input)
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

        public static bool IsNiceStringV2(string input)
        {
            bool hasValidRepeatingLetterPair = DetermineIfInputHasValidRepeatingLetterPair(input);

            if (!hasValidRepeatingLetterPair)
                return false;

            bool hasValid3LetterCombo = DetermineIfInputHasValid3LetterCombo(input);

            if (!hasValid3LetterCombo)
                return false;

            return true;
        }

        public static bool DetermineIfInputHasValid3LetterCombo(string input)
        {
            if (input.Length < 3)
                throw new ArgumentException();

            if (input.Length == 3)
                return input[0] == input[2];
            
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2])
                    //hasValid3LetterCombo = true;
                    return true;
            }

            return false;
        }

        public static bool DetermineIfInputHasValidRepeatingLetterPair(string input)
        {
            var letterPairs = EnumLetterPairs(input);

            var repeatingLetterPairs = letterPairs
                .GroupBy(x => x.ToString())
                .Where(x => x.Count() > 1)
                .ToList();

            foreach (var repeatingLetterPairGroup in repeatingLetterPairs)
            {
                //foreach repeating letter pair, make sure that there are at least 2 entries that have a delta SourceIndex > 1

                var combinations = repeatingLetterPairGroup.GetKCombs(2);

                foreach (var combo in combinations)
                {
                    var a = combo.ToList();

                    //get the delta SourceIndex of the two
                    var firstSource = combo.First().SourceIndex;
                    var lastSource = combo.Last().SourceIndex;

                    var delta = Math.Abs(firstSource - lastSource);

                    if (delta > 1)
                    {
                        //we've found a letter pair that works
                        //hasValidRepeatingLetterPair = true;
                        return true;
                    }
                }

            }

            return false;
        }

        private static IEnumerable<LetterPair> EnumLetterPairs(string input)
        {
            //loop through all lettes, except for the last one
            for (int i = 0; i < input.Length - 1; i++)
            {
                yield return new LetterPair(input[i], input[i + 1], i);
            }
        }

        public class LetterPair : IComparable, IComparable<LetterPair>
        {
            private string letters;

            public int SourceIndex { get; private set; }

            public LetterPair(char val1, char val2, int sourceIndex)
            {
                letters = val1.ToString() + val2.ToString();
                SourceIndex = sourceIndex;
            }

            public override string ToString()
            {
                return letters;
            }

            //int IComparable<LetterPair>.CompareTo(LetterPair other)
            //{
            //    return ToString().CompareTo(other.ToString());
            //}

            public int CompareTo(object obj)
            {
                //var other = (LetterPair)obj;
                //return ToString().CompareTo(other.ToString());

                return (this as IComparable<LetterPair>).CompareTo((LetterPair)obj);
            }

            int IComparable<LetterPair>.CompareTo(LetterPair other)
            {
                var aValue = letters + SourceIndex;
                var bValue = other.letters + other.SourceIndex;

                return aValue.CompareTo(bValue);

                //return ToString().CompareTo(other.ToString());
            }
        }
    }
}
