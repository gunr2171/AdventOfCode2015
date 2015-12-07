using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day07
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Wire
    {

    }

    public abstract class Gate
    {
        public Wire OutputWire { get; set; }
    }

    public class AndGate : Gate
    {

    }

    public class OrGate : Gate
    {

    }

    public class LeftShiftGate : Gate
    {

    }

    public class RightShftGate : Gate
    {

    }

    public class NotGate : Gate
    {

    }


}
