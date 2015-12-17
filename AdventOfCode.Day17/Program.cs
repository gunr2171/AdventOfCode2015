using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using TCL.Extensions;

namespace AdventOfCode.Day17
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class Storage
    {
        private List<Container> containers = new List<Container>();

        public void AddContainer(int containerSize)
        {
            containers.Add(new Container() { Capacity = containerSize });
        }

        public int GetContainerCombinationCount(int totalLiquidSize)
        {
            var workingCombinations = EnumCombinationsOfAllLenghts()
                .Where(x => x.Sum(c => c.Capacity) == totalLiquidSize);

            var comboCount = workingCombinations.Count();

            return comboCount;
        }

        private IEnumerable<IEnumerable<Container>> EnumCombinationsOfAllLenghts()
        {
            for (int i = 1; i < containers.Count; i++)
            {
                foreach (var combo in containers.GetKCombs(i))
                {
                    yield return combo;
                }
            }
        }
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
