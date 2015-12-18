using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day18
{
    class Program
    {
        static void Main(string[] args)
        {

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

            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    var litNeighborCount = CalculateLitNeighborsForLight(row, column);
                    var currentValue = lights[row, column];
                    var newValue = Processor.CalculateNewValueForLight(currentValue, litNeighborCount);

                    newBoard.lights[row, column] = newValue;
                }
            }

            return newBoard;
        }

        public int CalculateLitNeighborsForLight(int xIndex, int yIndex)
        {
            var allLights = from row in Enumerable.Range(0, gridSize)
                            from column in Enumerable.Range(0, gridSize)
                            select new { X = row, Y = column, Value = lights[row, column] };

            var neighbors = allLights
                .Where(l =>
                    (l.X == xIndex - 1 && l.Y == yIndex - 1) || //top left
                    (l.X == xIndex - 1 && l.Y == yIndex + 0) || //top
                    (l.X == xIndex - 1 && l.Y == yIndex + 1) || //top right
                    (l.X == xIndex + 0 && l.Y == yIndex + 1) || //right
                    (l.X == xIndex + 1 && l.Y == yIndex + 1) || //bottom right
                    (l.X == xIndex + 1 && l.Y == yIndex + 0) || //bottom
                    (l.X == xIndex + 1 && l.Y == yIndex - 1) || //bottom left
                    (l.X == xIndex + 0 && l.Y == yIndex - 1));  //left

            var litNeighbors = neighbors.Where(x => x.Value == true);
            var litNeighborCount = litNeighbors.Count();

            return litNeighborCount;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    var cellDisplayValue = lights[row, column]
                        ? "#"
                        : ".";

                    builder.Append(cellDisplayValue);
                }

                if (row != gridSize - 1) //only add a new line if you are not on the last one
                    builder.AppendLine();
            }

            return builder.ToString();
        }
    }

    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> EnumValues<T>(T[,] source, int size)
        {
            var values = from x in Enumerable.Range(0, size)
                         from y in Enumerable.Range(0, size)
                         orderby x
                         orderby y
                         select new { X = x, Y = y, Value = source[x, y] };

            var result = values
                .GroupBy(x => x.X)
                .Select(x => x.Select(y => y.Value));

            return result;
        }
    }
}
