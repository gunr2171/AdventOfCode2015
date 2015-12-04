using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var part1Answer = Processor.GetLowestConcatNumberForSeed("yzbqklnj", 5);
            var part2Answer = Processor.GetLowestConcatNumberForSeed("yzbqklnj", 6);
        }
    }

    public static class Processor
    {
        public static int GetLowestConcatNumberForSeed(string seed, int hashLeadingZeros)
        {
            //hard limit ourselves
            var suffixNumbers = Enumerable.Range(1, int.MaxValue);

            var lowestWorkingNumber = suffixNumbers
                .Where(x => CheckHash(seed, x, hashLeadingZeros))
                .First();

            return lowestWorkingNumber;
        }

        private static bool CheckHash(string seed, int suffix, int hashLeadingZeros)
        {
            using (var hasher = MD5.Create())
            {
                var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(seed + suffix));

                //we only need to check the first (hashLeadingZeros) characters
                return hashBytes
                    .Select(x => x.ToString("x2"))
                    .SelectMany(x => x)
                    .Take(hashLeadingZeros)
                    .All(x => x == '0');
            }
        }
    }
}
