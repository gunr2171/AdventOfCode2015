using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day20
{
    class Program
    {
        static void Main(string[] args)
        {
            var targetNumber = 36000000;

            var part1Answer = Processor.Run(targetNumber);
        }
    }

    public static class Processor
    {
        public static int Run(int targetPresentCount)
        {
            var houseValues = new Dictionary<int, int>();

            for (int i = 1; i <= targetPresentCount / 10; i++)
            {
                for (int j = i; j <= targetPresentCount / 10; j += i)
                {
                    if (!houseValues.ContainsKey(j))
                    {
                        houseValues.Add(j, i * 10);
                    }
                    else
                    {
                        houseValues[j] += i * 10;
                    }
                }
            }

            var smallestHouseWithTargetValue = houseValues
                .Where(x => x.Value >= targetPresentCount)
                .Select(x => x.Key)
                .Min();

            return smallestHouseWithTargetValue;
        }


        public static int GetPresentCountForHouseNumber(int houseNumber)
        {
            var presentCount = 0;

            for (int i = 1; i <= houseNumber; i += houseNumber)
            {
                presentCount += i * 10;
            }

            return presentCount;
        }
    }
}
