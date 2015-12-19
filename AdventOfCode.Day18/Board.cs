using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.Extensions;

namespace AdventOfCode.Day18
{
    public class Board
    {
        protected int gridSize;

        public Board(int side)
        {
            lights = new bool[side, side];
            gridSize = side;
        }

        protected bool[,] lights;

        public virtual void SetBoardValues(string rawMatrix)
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

        public virtual bool CalculateNewValueForLight(bool currentValue, int litNeighborCount, int x, int y)
        {
            //this version of the method does not care about the x and y values

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

        /// <summary>
        /// Returns a new board based on this board but animated by one step.
        /// </summary>
        /// <returns></returns>
        public Board StepBoard()
        {
            var newBoard = MakeNewBoard();
            Array.Copy(this.lights, newBoard.lights, gridSize); //copy over the light values

            foreach (var lightInfo in EnumLights())
            {
                var litNeighborCount = CalculateLitNeighborsForLight(lightInfo.XIndex, lightInfo.YIndex);
                var currentValue = lights[lightInfo.XIndex, lightInfo.YIndex];
                var newValue = CalculateNewValueForLight(currentValue, litNeighborCount, lightInfo.XIndex, lightInfo.YIndex);

                newBoard.lights[lightInfo.XIndex, lightInfo.YIndex] = newValue;
            }

            return newBoard;
        }

        protected virtual Board MakeNewBoard()
        {
            return new Board(gridSize);
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
            var hasLeft = yIndex > 0;
            var hasRight = yIndex < gridSize - 1;
            var hasTop = xIndex > 0;
            var hasBottom = xIndex < gridSize - 1;

            var litCount = 0;

            //cardinal directions
            if (hasTop && lights[xIndex - 1, yIndex]) litCount++;
            if (hasBottom && lights[xIndex + 1, yIndex]) litCount++;
            if (hasLeft && lights[xIndex, yIndex - 1]) litCount++;
            if (hasRight && lights[xIndex, yIndex + 1]) litCount++;

            //diagonals
            if (hasTop && hasLeft && lights[xIndex - 1, yIndex - 1]) litCount++;
            if (hasTop && hasRight && lights[xIndex - 1, yIndex + 1]) litCount++;
            if (hasBottom && hasLeft && lights[xIndex + 1, yIndex - 1]) litCount++;
            if (hasBottom && hasRight && lights[xIndex + 1, yIndex + 1]) litCount++;

            return litCount;
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
