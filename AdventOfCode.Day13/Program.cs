using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;
using AdventOfCode.Common;

namespace AdventOfCode.Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var relationships = File.ReadAllLines("Input.txt");

            var table = new Table();

            foreach (var relationship in relationships)
            {
                table.AddRelationship(relationship);
            }

            var part1Answer = table.FindBestNetHappinessArrangement();

            table.AddNeutralMember();

            var part2Answer = table.FindBestNetHappinessArrangement();
        }
    }

    public class Table
    {
        private List<Relationship> relationships = new List<Relationship>();

        public void AddRelationship(string rawRelationship)
        {
            relationships.Add(new Relationship(rawRelationship));
        }

        public void AddNeutralMember()
        {
            var allTableMembers = relationships
                .Select(x => x.AffectedPerson)
                .Distinct()
                .ToList();

            foreach (var tableMember in allTableMembers)
            {
                //construct a relationship where you are the affected member
                var selfAffectedRelationship = new Relationship("self", tableMember, 0);
                relationships.Add(selfAffectedRelationship);

                //construct a relationship where the other member is the affected member
                var otherAffectedRelationship = new Relationship(tableMember, "self", 0);
                relationships.Add(otherAffectedRelationship);
            }
        }

        public int FindBestNetHappinessArrangement()
        {
            var allTableMembers = relationships
                .Select(x => x.AffectedPerson)
                .Distinct()
                .ToList();

            var allArrangements = allTableMembers.GetPermutations(allTableMembers.Count);

            var bestNetHappiness = allArrangements
                .Select(x => DetermineNetHappinessForArangement(x.ToList()))
                .Max();

            return bestNetHappiness;
        }

        private int DetermineNetHappinessForArangement(List<string> members)
        {
            var netHappiness = 0;

            for (int i = 0; i < members.Count - 1; i++) //go through all members (except for the last one)
            {
                //compare this affected by the next
                netHappiness += GetHappinessOffset(members[i], members[i + 1]);

                //compare the next affected by this
                netHappiness += GetHappinessOffset(members[i + 1], members[i]);
            }

            //compare the first affected by the last
            netHappiness += GetHappinessOffset(members.First(), members.Last());

            //compare the last affected by the first
            netHappiness += GetHappinessOffset(members.Last(), members.First());

            return netHappiness;
        }

        private int GetHappinessOffset(string affectedPerson, string influencer)
        {
            var relationship = relationships
                .Where(x => x.AffectedPerson == affectedPerson)
                .Where(x => x.Influencer == influencer)
                .Single();

            return relationship.HappinessOffset;
        }
    }

    public class Relationship
    {
        /// <summary>
        /// The person who's happiness will be modified by relationship
        /// </summary>
        public string AffectedPerson { get; set; }

        /// <summary>
        /// The person who will be affecting the Affected Person.
        /// </summary>
        public string Influencer { get; set; }

        public int HappinessOffset { get; set; }

        public Relationship(string rawRelationship)
        {
            var match = Regex.Match(rawRelationship, @"^(\w+) would (gain|lose) (\d+) happiness units by sitting next to (\w+).$");

            AffectedPerson = match.Groups[1].Value;
            Influencer = match.Groups[4].Value;

            HappinessOffset = match.Groups[3].Value.Parse<int>();

            if (match.Groups[2].Value == "lose")
                HappinessOffset = -HappinessOffset; //negitize the value
        }

        public Relationship(string affected, string influencer, int happinessOffset)
        {
            AffectedPerson = affected;
            Influencer = influencer;
            HappinessOffset = happinessOffset;
        }
    }
}
