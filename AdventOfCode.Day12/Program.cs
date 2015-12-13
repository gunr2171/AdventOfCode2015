using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputContent = File.ReadAllText("Input.txt");

            var part1Answer = Processor.GetSumOfAllNumbersInJson(inputContent);            
        }
    }

    public static class Processor
    {
        public static int GetSumOfAllNumbersInJson(string input)
        {
            JContainer json;

            if (input.StartsWith("{")) //this is an object
                json = JObject.Parse(input);
            else //starts with "[", so it's an array
                json = JArray.Parse(input);

            var sum = json.Descendants()
                .Where(x => x.Type == JTokenType.Integer)
                .Select(x => x.Value<int>())
                .Sum();

            return sum;
        }
    }
}
