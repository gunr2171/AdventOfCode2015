using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;

namespace AdventOfCode.Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var part1Board = new OnOffBoard();
            var part2Board = new BrightnessBoard();
            var instructions = File.ReadAllLines("Input.txt");

            foreach (var instruction in instructions)
            {
                part1Board.ApplyInstruction(instruction);
                part2Board.ApplyInstruction(instruction);
            }

            var part1Answer = part1Board.LightsTurnedOn();
            var part2Answer = part2Board.TotalBrightness();
        }
    }

    public abstract class Board<T>
    {
        protected T[,] lights = new T[1000, 1000];

        public void ApplyInstruction(string instruction)
        {
            var instructionPattern = new Regex(@"^(turn off|turn on|toggle) (\d+),(\d+) through (\d+),(\d+)$");
            var match = instructionPattern.Match(instruction);

            var instructionType = match.Groups[1].Value;
            var point1X = match.Groups[2].Value.Parse<int>();
            var point1Y = match.Groups[3].Value.Parse<int>();
            var point2X = match.Groups[4].Value.Parse<int>();
            var point2Y = match.Groups[5].Value.Parse<int>();

            for (int x = point1X; x <= point2X; x++)
            {
                for (int y = point1Y; y <= point2Y; y++)
                {
                    //on each cell, determine what needs to be done
                    SetLightValue(x, y, instructionType);
                }
            }
        }

        protected abstract void SetLightValue(int x, int y, string instructionType);

        protected IEnumerable<T> EnumLightValues()
        {
            var allLights = from xVal in Enumerable.Range(0, 1000)
                            from yVal in Enumerable.Range(0, 1000)
                            select lights[xVal, yVal];

            return allLights;
        }
    }

    public class OnOffBoard : Board<bool>
    {
        public int LightsTurnedOn()
        {
            var poweredLightsCount = EnumLightValues()
                .Where(x => x == true)
                .Count();

            return poweredLightsCount;
        }

        protected override void SetLightValue(int x, int y, string instructionType)
        {
            switch (instructionType)
            {
                case "turn on":
                    lights[x, y] = true;
                    break;
                case "turn off":
                    lights[x, y] = false;
                    break;
                case "toggle":
                    lights[x, y] = !lights[x, y];
                    break;
            }
        }
    }

    public class BrightnessBoard : Board<int>
    {
        protected override void SetLightValue(int x, int y, string instructionType)
        {
            switch (instructionType)
            {
                case "turn on":
                    lights[x, y]++;
                    break;
                case "turn off":
                    lights[x, y]--;
                    if (lights[x, y] < 0)
                        lights[x, y] = 0;
                    break;
                case "toggle":
                    lights[x, y] += 2;
                    break;
            }
        }

        public int TotalBrightness()
        {
            var result = EnumLightValues().Sum();
            return result;
        }
    }
}
