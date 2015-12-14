using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;

namespace AdventOfCode.Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines("Input.txt");

            var race = new Race();

            foreach(var line in inputLines)
            {
                race.AddReindeer(line);
            }

            var part1Answer = race.CalculateDistanceOfFurthestReindeerAtMoment(2503);
            var part2Answer = race.CalculateMostPointsAtMoment(2503);
        }
    }

    public class Race
    {
        List<Reindeer> reindeers = new List<Reindeer>();

        public void AddReindeer(string rawInput)
        {
            reindeers.Add(new Reindeer(rawInput));
        }

        public int CalculateDistanceOfFurthestReindeerAtMoment(int secondsElasped)
        {
            List<Racer> racers = SimulateRace(secondsElasped);

            //done simulating the race, find the winner
            var furthestDistance = racers
                .Select(x => x.CurrentDistance)
                .Max();

            return furthestDistance;
        }

        public int CalculateMostPointsAtMoment(int secondsElasped)
        {
            List<Racer> racers = SimulateRace(secondsElasped);

            var mostPoints = racers
                .Select(x => x.Score)
                .Max();

            return mostPoints;
        }

        private List<Racer> SimulateRace(int secondsElasped)
        {
            //value represents the current distance
            var racers = reindeers
                .Select(x => new Racer
                {
                    Reindeer = x,
                    IsFlying = true,
                    NextStatusChangeAt = x.MaxTravelTime + 1,
                    CurrentDistance = 0,
                    Score = 0
                })
                .ToList();

            foreach (var elaspedSecond in Enumerable.Range(1, secondsElasped))
            {
                foreach (var racer in racers)
                {
                    //is it time for this racer to change state?
                    if (racer.NextStatusChangeAt == elaspedSecond)
                    {
                        racer.IsFlying = !racer.IsFlying; //flip the value
                        racer.NextStatusChangeAt = racer.IsFlying
                            ? racer.Reindeer.MaxTravelTime + elaspedSecond
                            : racer.Reindeer.WaitPeriod + elaspedSecond;
                    }

                    if (racer.IsFlying)
                    {
                        racer.CurrentDistance += racer.Reindeer.Speed;
                    }
                }

                //figure out the furthest distance
                var furthestDistance = racers
                    .Select(x => x.CurrentDistance)
                    .Max();

                //get all reindeer with that distance
                var racersInLead = racers
                    .Where(x => x.CurrentDistance == furthestDistance);

                //go through each of them and give them a point
                foreach (var racerInLead in racersInLead)
                {
                    racerInLead.Score += 1;
                }
            }

            return racers;
        }
    }

    public class Racer
    {
        public Reindeer Reindeer { get; set; }
        public bool IsFlying { get; set; }
        public int NextStatusChangeAt { get; set; }
        public int CurrentDistance { get; set; }
        public int Score { get; set; }
    }

    public class Reindeer
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int MaxTravelTime { get; set; }
        public int WaitPeriod { get; set; }

        public Reindeer(string rawInfo)
        {
            var match = Regex.Match(rawInfo, @"(\w+) can fly (\d+) km\/s for (\d+) seconds, but then must rest for (\d+) seconds\.");

            Name = match.Groups[1].Value;
            Speed = match.Groups[2].Value.Parse<int>();
            MaxTravelTime = match.Groups[3].Value.Parse<int>();
            WaitPeriod = match.Groups[4].Value.Parse<int>();
        }
    }
}
