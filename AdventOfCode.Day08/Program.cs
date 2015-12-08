using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var entries = File.ReadAllLines("Input.txt").ToList();
        }
    }

    public static class Processor
    {
        public static int GetLiteralCharacterCount(string input)
        {
            return input.Length;
        }

        public static int GetMemoryCharacterCount(string intput)
        {
            throw new NotImplementedException();
        }
    }
}
