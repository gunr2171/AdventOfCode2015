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
            var initialBoard = new Board(100);
            initialBoard.SetBoardValues(input);

            var finishedBoard = Processor.RunBoardAnimation(initialBoard, 100);

            var part1Answer = Processor.GetCountOfLitLights(finishedBoard);
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

        public static bool CalculateNewValueForLight(bool currentValue, int litNeighborCount)
        {
            if (currentValue == true && (litNeighborCount == 2 || litNeighborCount == 3))
            {
                return true;
            }

            if (currentValue == false && litNeighborCount == 3)
            {
                return true;
            }

            //else, turn off
            return false;
        }

        public static int GetCountOfLitLights(Board board)
        {
            return board.EnumLights()
                .Where(l => l.Value == true)
                .Count();
        }
    }

    public class Board
    {
        private int gridSize;

        public Board(int side)
        {
            lights = new bool[side, side];
            gridSize = side;
        }

        private bool[,] lights;

        public void SetBoardValues(string rawMatrix)
        {
            var rows = rawMatrix
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    lights[row, column] = rows[row][column] == '#';
                }
            }
        }

        /// <summary>
        /// Returns a new board based on this board but animated by one step.
        /// </summary>
        /// <returns></returns>
        public Board StepBoard()
        {
            var newBoard = new Board(gridSize);
            Array.Copy(this.lights, newBoard.lights, gridSize); //copy over the light values

            foreach (var lightInfo in EnumLights())
            {
                var litNeighborCount = CalculateLitNeighborsForLight(lightInfo.XIndex, lightInfo.YIndex);
                var currentValue = lights[lightInfo.XIndex, lightInfo.YIndex];
                var newValue = Processor.CalculateNewValueForLight(currentValue, litNeighborCount);

                newBoard.lights[lightInfo.XIndex, lightInfo.YIndex] = newValue;
            }

            return newBoard;
        }

        public class LightInfo
        {
            public int XIndex { get; set; }
            public int YIndex { get; set; }
            public bool Value { get; set; }
        }

        public IEnumerable<LightInfo> EnumLights()
        {
            var allLights = from row in Enumerable.Range(0, gridSize)
                            from column in Enumerable.Range(0, gridSize)
                            select new LightInfo { XIndex = row, YIndex = column, Value = lights[row, column] };

            return allLights;
        }

        public int CalculateLitNeighborsForLight(int xIndex, int yIndex)
        {
            var neighbors = EnumLights()
                .Where(l =>
                    (l.XIndex == xIndex - 1 && l.YIndex == yIndex - 1) || //top left
                    (l.XIndex == xIndex - 1 && l.YIndex == yIndex + 0) || //top
                    (l.XIndex == xIndex - 1 && l.YIndex == yIndex + 1) || //top right
                    (l.XIndex == xIndex + 0 && l.YIndex == yIndex + 1) || //right
                    (l.XIndex == xIndex + 1 && l.YIndex == yIndex + 1) || //bottom right
                    (l.XIndex == xIndex + 1 && l.YIndex == yIndex + 0) || //bottom
                    (l.XIndex == xIndex + 1 && l.YIndex == yIndex - 1) || //bottom left
                    (l.XIndex == xIndex + 0 && l.YIndex == yIndex - 1));  //left

            var litNeighbors = neighbors.Where(x => x.Value == true);
            var litNeighborCount = litNeighbors.Count();

            return litNeighborCount;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            var result = EnumLights()
                .OrderBy(l => l.XIndex)
                .ThenBy(l => l.YIndex)
                .GroupBy(l => l.XIndex)
                .Select(g => g.Select(x => x.Value ? "#" : ".").ToCSV(""))
                .ToCSV(Environment.NewLine);

            return result;
        }
    }
}
