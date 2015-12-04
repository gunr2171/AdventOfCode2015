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
            var part1Answer = Processor.GetLowestConcatNumberForSeed("yzbqklnj");
        }
    }

    public static class Processor
    {
        public static int GetLowestConcatNumberForSeed(string seed)
        {
            //hard limit ourselves at 4 million
            var suffixNumbers = Enumerable.Range(1, 4000000);

            var lowestWorkingNumber = suffixNumbers
                .Where(x => CheckHash(seed, x))
                .First();

            return lowestWorkingNumber;
        }

        private static bool CheckHash(string seed, int suffix)
        {
            using (var hasher = MD5.Create())
            {
                var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(seed + suffix));

                var builder = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    builder.Append(hashByte.ToString("x2"));
                }

                var fullHash = builder.ToString();
                return fullHash.Substring(0, 5) == "00000";
            }
        }
    }
}
