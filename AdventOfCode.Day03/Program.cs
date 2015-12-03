using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadAllText("Input.txt");

            var part1Answer = Processor.GetUniqueHouseCount(instructions, 1);
            var part2Answer = Processor.GetUniqueHouseCount(instructions, 2);
        }
    }

    public static class Processor
    {
        public static int GetUniqueHouseCount(string instructions, int deliverers)
        {
            var visitedHouses = new List<Point>();

            var currentPositions = Enumerable.Range(0, deliverers)
                .Select(x => new Point())
                .ToList();

            //foreach deliverer, add in the initial house
            visitedHouses.AddRange(currentPositions);

            var instructionEntries = instructions
                .Select((x, i) => new { Instruction = x, Index = i });

            foreach (var instructionEntry in instructionEntries)
            {
                var delivererIndex = instructionEntry.Index % deliverers;
                var currentPosition = currentPositions[instructionEntry.Index % deliverers];

                ApplyInstructionToCurrentPosition(ref currentPosition, instructionEntry.Instruction);
                visitedHouses.Add(new Point(currentPosition.X, currentPosition.Y));

                currentPositions[delivererIndex] = currentPosition;
            }

            //gone through all the instructions, determine the unique houses
            var uniqueHousesVisited = visitedHouses
                .Distinct()
                .Count();

            return uniqueHousesVisited;
        }

        private static void ApplyInstructionToCurrentPosition(ref Point currentCoordinate, char instruction)
        {
            switch (instruction)
            {
                case '<':
                    currentCoordinate.X--;
                    break;
                case '>':
                    currentCoordinate.X++;
                    break;
                case '^':
                    currentCoordinate.Y--;
                    break;
                case 'v':
                    currentCoordinate.Y++;
                    break;
            }
        }


    }
}
