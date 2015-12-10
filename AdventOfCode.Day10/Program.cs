using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "1321131112";

            var part1TempAnswer = input;

            for (int i = 0; i < 40; i++)
            {
                part1TempAnswer = Processor.GetNextLookAndSayValue(part1TempAnswer);
            }

            var part1Answer = part1TempAnswer.Length;

            //ten more times
            var part2TempAnswer = part1TempAnswer;
            for (int i = 0; i < 10; i++)
            {
                part2TempAnswer = Processor.GetNextLookAndSayValue(part2TempAnswer);
            }

            var part2Answer = part2TempAnswer.Length;
        }
    }

    public static class Processor
    {
        public static string GetNextLookAndSayValue(string input)
        {
            var builder = new StringBuilder();

            var currentRunChar = input[0];
            var currentRunCount = 0;

            foreach (var currentLetter in input)
            {
                if (currentLetter == currentRunChar)
                {
                    currentRunCount++;
                }
                else
                {
                    //dump the info about the last run
                    builder.Append(currentRunCount);
                    builder.Append(currentRunChar);

                    //start a new run
                    currentRunChar = currentLetter;
                    currentRunCount = 1;
                }
            }

            //done with the loop, dump the info on the last group
            builder.Append(currentRunCount);
            builder.Append(currentRunChar);

            var result = builder.ToString();
            return result;
        }
    }
}
