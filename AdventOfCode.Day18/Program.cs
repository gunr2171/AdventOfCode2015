using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.Extensions;

namespace AdventOfCode.Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("Input.txt");

            var initialPart1Board = new Board(100);
            initialPart1Board.SetBoardValues(input);
            var finishedPart1Board = Processor.RunBoardAnimation(initialPart1Board, 100);

            var part1Answer = Processor.GetCountOfLitLights(finishedPart1Board);

            var initialPart2Board = new StuckBoard(100);
            initialPart2Board.SetBoardValues(input);
            var finishedPart2Board = Processor.RunBoardAnimation(initialPart1Board, 100);

            var part2Answer = Processor.GetCountOfLitLights(finishedPart1Board);
        }
    }

    public static class Processor
    {
        public static Board RunBoardAnimation(Board initialBoardLayout, int stepCount)
        {
            var steps = Enumerable.Range(1, stepCount);
            var workingBoard = initialBoardLayout;

            foreach (var step in steps)
            {
                workingBoard = workingBoard.StepBoard();
            }

            return workingBoard;
        }

        public static int GetCountOfLitLights(Board board)
        {
            return board.EnumLights()
                .Where(l => l.Value == true)
                .Count();
        }
    }
}
