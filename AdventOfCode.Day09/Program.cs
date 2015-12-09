using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;

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

    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetPermutationsWithRept<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutationsWithRept(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetKCombsWithRept<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetKCombsWithRept(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetKCombs<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetKCombs(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
