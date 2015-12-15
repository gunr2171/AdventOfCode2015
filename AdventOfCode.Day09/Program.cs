using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;
using AdventOfCode.Common;

namespace AdventOfCode.Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            var distanceRecords = File.ReadAllLines("Input.txt");
            var calculator = new Calculator();

            foreach(var record in distanceRecords)
            {
                calculator.AddDistanceRecord(record);   
            }

            var part1Answer = calculator.CalculateShortestRoute();
            var part2Answer = calculator.CalculateLongestRoute();
        }
    }

    public class Calculator
    {
        private List<CityDistance> CityDistances;

        public Calculator()
        {
            CityDistances = new List<CityDistance>();
        }

        public void AddDistanceRecord(string distanceRecord)
        {
            CityDistances.Add(new CityDistance(distanceRecord));
        }

        public int CalculateShortestRoute()
        {
            var cityCount = EnumCities().Count();
            var allCityCombinations = EnumCities().GetPermutations(cityCount);

            var shortestDistance = allCityCombinations
                .Select(x => GenerateDistanceFromCityList(x))
                .Min();

            return shortestDistance;
        }

        public int CalculateLongestRoute()
        {
            var cityCount = EnumCities().Count();
            var allCityCombinations = EnumCities().GetPermutations(cityCount);

            var shortestDistance = allCityCombinations
                .Select(x => GenerateDistanceFromCityList(x))
                .Max();

            return shortestDistance;
        }

        public int GenerateDistanceFromCityList(IEnumerable<string> cityList)
        {
            var list = cityList.ToList();
            var runningSum = 0;

            for (int i = 0; i < list.Count - 1; i++)
            {
                var distanceRecord = CityDistances
                    .Where(x => x.Cities.Contains(list[i]))
                    .Where(x => x.Cities.Contains(list[i + 1]))
                    .Single();

                runningSum += distanceRecord.Distance;
            }

            return runningSum;
        }

        private IEnumerable<string> EnumCities()
        {
            return CityDistances
                .SelectMany(x => x.Cities)
                .Distinct();
        }
    }

    public class CityDistance
    {
        public List<string> Cities { get; set; }
        public int Distance { get; set; }

        public CityDistance(string rawDistanceRecord)
        {
            Cities = new List<string>();

            var match = Regex.Match(rawDistanceRecord, @"^(\w+) to (\w+) = (\d+)$");

            Cities.Add(match.Groups[1].Value);
            Cities.Add(match.Groups[2].Value);

            Distance = match.Groups[3].Value.Parse<int>();
        }
    }
}
