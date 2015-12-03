using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadAllText("Input.txt");

            var part1Answer = Processor.GetFinalFloorNumber(instructions);
            var part2Answer = Processor.GetInstructionPositionThatLeadToBasement(instructions);
        }
    }

    public static class Processor
    {
        /// <summary>
        /// After all the ups and downs have been followed, what is the floor the person ends up on?
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public static int GetFinalFloorNumber(string instructions)
        {
            var finalFloorNumber = instructions
                .Select(x => x == '(' ? 1 : -1)
                .Sum();

            return finalFloorNumber;
        }

        /// <summary>
        /// Gets the 1-based position of the instruction that caused the first move into the basement (-1)
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public static int GetInstructionPositionThatLeadToBasement(string instructions)
        {
            var fixedInstructions = instructions
                .Select(x => x == '(' ? 1 : -1)
                .ToList();

            int instructionIndex = -1;
            int currentFloor = 0;

            while (currentFloor > -1)
            {
                instructionIndex++;
                currentFloor += fixedInstructions[instructionIndex];
            }

            return instructionIndex + 1;
        }
    }
}
