using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using TCL.Extensions;
using System.IO;

namespace AdventOfCode.Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var storage = new Storage(150);

            var containers = File.ReadAllLines("Input.txt")
                .Select(x => x.Parse<int>());

            foreach (var container in containers)
            {
                storage.AddContainer(container);
            }

            var part1Answer = storage.GetTotalWorkingContainerCombinationCount();
        }
    }

    public class Storage
    {
        private List<Container> containers = new List<Container>();
        private int totalLiquidSize;

        public Storage(int totalLiquidSize)
        {
            this.totalLiquidSize = totalLiquidSize;
        }

        /// <summary>
        /// Key is the container count. Value is all the combinations for that count.
        /// </summary>
        private Dictionary<int, List<List<Container>>> cachedCombos = new Dictionary<int, List<List<Container>>>();

        public void AddContainer(int containerSize)
        {
            containers.Add(new Container() { Capacity = containerSize });
        }

        public int GetTotalWorkingContainerCombinationCount()
        {
            var containerCounts = Enumerable.Range(1, containers.Count);

            var totalComboCount = containerCounts
                .Select(x => ComputeWorkingCombosForContainerCount(x))
                .Select(x => x.Count)
                .Sum();

            return totalComboCount;
        }

        public int GetMinimumNumberOfContainersToHoldLiquid()
        {
            for (int containerCount = 1; containerCount < containers.Count; containerCount++)
            {
                //I would change this to draw from an IEnumerable instead of a list, but because
                //of the puzzle, the values are already going to be cached, so it doesn't matter.
                //in fact, it would be faster this way.
                var doesContainerNumberContainWorkingCombo = ComputeWorkingCombosForContainerCount(containerCount)
                    .Where(x => x.Sum(c => c.Capacity) == totalLiquidSize)
                    .Any();

                if (doesContainerNumberContainWorkingCombo)
                {
                    return containerCount;
                }
            }

            throw new Exception("Could not find working combo");
        }

        public int GetCountOfWorkingCombosForContainerCount(int numOfContainers)
        {
            var workingCombosCount = ComputeWorkingCombosForContainerCount(numOfContainers).Count();

            return workingCombosCount;
        }

        private List<List<Container>> ComputeWorkingCombosForContainerCount(int numOfContainers)
        {
            if (!cachedCombos.ContainsKey(numOfContainers))
            {
                //we have not computed the values yet, do that now, cache it, and return it
                var computedWorkingCombos = containers.GetKCombs(numOfContainers)
                    .Where(x => x.Sum(c => c.Capacity) == totalLiquidSize)
                    .Select(x => x.ToList())
                    .ToList();

                cachedCombos.Add(numOfContainers, computedWorkingCombos);
            }

            //return what is cached
            return cachedCombos[numOfContainers];
        }

        //private IEnumerable<IEnumerable<Container>> EnumCombinationsOfAllLenghts()
        //{
        //    for (int i = 1; i < containers.Count; i++)
        //    {
        //        foreach (var combo in ComputeWorkingCombosForContainerCount(i))
        //        {
        //            yield return combo;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// This entire class exists because you can have multiple containers
    /// of the same capacity, but are identified differently.
    /// </summary>
    public class Container : IComparable, IComparable<Container>
    {
        private Guid identifier;

        public int Capacity { get; set; }

        public Container()
        {
            identifier = Guid.NewGuid();
        }

        public int CompareTo(object obj)
        {
            return CompareTo((Container)obj);
        }

        public int CompareTo(Container other)
        {
            return MakeComparingString().CompareTo(other.MakeComparingString());
        }

        private string MakeComparingString()
        {
            return "{0}:{1}".FormatInline(Capacity, identifier.ToString());
        }

    }

}
