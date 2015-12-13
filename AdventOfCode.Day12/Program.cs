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
            var part2Answer = Processor.GetSumOfAllNonRedNumbers(inputContent);
        }
    }

    public static class Processor
    {
        public static int GetSumOfAllNumbersInJson(string input)
        {
            var json = ParseJsonString(input);

            var sum = json.Descendants()
                .Where(x => x.Type == JTokenType.Integer)
                .Select(x => x.Value<int>())
                .Sum();

            return sum;
        }

        private static JContainer ParseJsonString(string input)
        {
            JContainer json;

            if (input.StartsWith("{")) //this is an object
                json = JObject.Parse(input);
            else //starts with "[", so it's an array
                json = JArray.Parse(input);

            return json;
        }

        public static int GetSumOfAllNonRedNumbers(string input)
        {
            var json = ParseJsonString(input);

            var nonRedSum = json
                .Descendants()
                .Where(x => x.Type == JTokenType.Integer)
                .Select(x => GetValueOfIntegerNode(x))
                .Sum();

            return nonRedSum;
        }

        private static int GetValueOfIntegerNode(JToken node)
        {
            //we need to look up the node's ancestors
            var objectAncestors = node.Ancestors()
                .Where(x => x.Type == JTokenType.Object);

            //check through each of them to see if they contain a "red"
            foreach (var oAncestor in objectAncestors)
            {
                var valuesInObject = oAncestor
                    .Select(x => x as JProperty)
                    .Select(x => x.Value);

                var anyRedValues = valuesInObject
                    .Where(x => x.Type == JTokenType.String)
                    .Where(x => x.Value<string>() == "red")
                    .Any();

                if (anyRedValues)
                {
                    //one of the ancestors to this node has a red value,
                    //we can't use this one
                    return 0;
                }
            }

            //checked all the ancestors to this value, none have "red"
            //return the value
            return node.Value<int>();
        }
    }
}
