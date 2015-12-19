using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day18
{
    public class StuckBoard : Board
    {
        public StuckBoard(int side) : base(side) { }

        public override bool CalculateNewValueForLight(bool currentValue, int litNeighborCount, int x, int y)
        {
            if (
                (x == 0 && y == 0) ||
                (x == gridSize - 1 && y == 0) ||
                (x == 0 && y == gridSize - 1) ||
                (x == gridSize - 1 && y == gridSize - 1))
            {
                //light is stuck on
                return true;
            }

            return base.CalculateNewValueForLight(currentValue, litNeighborCount, x, y);
        }

        public override void SetBoardValues(string rawMatrix)
        {
            base.SetBoardValues(rawMatrix);

            //after all the initial values have been set, ensure the corners are on
            lights[0, 0] = true;
            lights[0, gridSize - 1] = true;
            lights[gridSize - 1, 0] = true;
            lights[gridSize - 1, gridSize - 1] = true;
        }

        protected override Board MakeNewBoard()
        {
            return new StuckBoard(gridSize);
        }
    }
}
