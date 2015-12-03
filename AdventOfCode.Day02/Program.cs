using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines("Input.txt");

            var gifts = inputLines
                .Select(x => x.Split('x')
                    .Select(y => int.Parse(y))
                    .ToList())
                .Select(x => new Gift(x[0], x[1], x[2]))
                .ToList();

            var totalWrappingPaper = gifts
                .Select(x => x.CalculateRequiredWrappingPaper())
                .Sum();

            var totalRibbon = gifts
                .Select(x => x.CalculateRequiredRibbon())
                .Sum();
        }
    }

    public class Gift
    {
        private List<int> dimentions = new List<int>();

        public int Length { get { return dimentions[0]; } }
        public int Width { get { return dimentions[1]; } }
        public int Height { get { return dimentions[2]; } }

        public Gift(int length, int width, int height)
        {
            dimentions.Add(length);
            dimentions.Add(width);
            dimentions.Add(height);
        }

        /// <summary>
        /// Returns all the combinations of dimensions multiplied by each other.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> EnumSides()
        {
            yield return Length * Width;
            yield return Width * Height;
            yield return Height * Length;
        }

        public int CalculateRequiredWrappingPaper()
        {
            var smallestSide = EnumSides().Min();

            var sideSum = EnumSides()
                .Select(x => x * 2)
                .Sum();

            var result = sideSum + smallestSide;
            return result;
        }

        public int CalculateRequiredRibbon()
        {
            var shortestDimentions = dimentions
                .OrderBy(x => x)
                .Take(2)
                .ToList();

            var wrappingRibbon = shortestDimentions
                .Select(x => x * 2)
                .Sum();

            var bowLength = Height * Width * Length;

            var result = wrappingRibbon + bowLength;
            return result;
        }
    }
}
