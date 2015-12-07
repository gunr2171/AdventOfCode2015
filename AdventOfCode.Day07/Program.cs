using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;

namespace AdventOfCode.Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("Input.txt");

            var part1Board = new CircuitBoard();

            foreach (var instruction in instructions)
            {
                part1Board.ApplyInstruction(instruction);
            }

            var part1Answer = part1Board.GetWireSignal("a");
        }
    }

    public class CircuitBoard
    {
        private List<Wire> wires;

        public CircuitBoard()
        {
            wires = new List<Wire>();
        }

        public void ApplyInstruction(string instruction)
        {
            var parsedInstruction = new Instruction(instruction, wires);

            //run the instruction
            if (parsedInstruction.Operation != null)
            {
                //run the operation
                var left = parsedInstruction.LeftInputWire?.Value ?? parsedInstruction.LeftInputValue;
                var right = parsedInstruction.RightInputWire?.Value ?? parsedInstruction.RightInputValue;

                parsedInstruction.OutputWire.Value = parsedInstruction.Operation.CalculateOutputWireSignal(left, right);
            }
            else
            {
                //set the value
                var valueToSet = parsedInstruction.LeftInputWire?.Value ?? parsedInstruction.LeftInputValue;
                parsedInstruction.OutputWire.Value = valueToSet.Value;
            }
        }



        public ushort GetWireSignal(string wireName)
        {
            return wires.Single(x => x.Name == wireName).Value;
        }


    }

    public class Instruction
    {
        public Wire LeftInputWire { get; set; }
        public ushort? LeftInputValue { get; set; }

        public Wire RightInputWire { get; set; }
        public ushort? RightInputValue { get; set; }

        public Wire OutputWire { get; set; }
        public Gate Operation { get; set; }

        public Instruction(string rawValue, List<Wire> wires)
        {
            var match = Regex.Match(rawValue, @"^(?:(?:([a-z]+?)|(\d+?)) )?(?:(AND|OR|LSHIFT|RSHIFT|NOT) )?(?:(?:([a-z]+?)|(\d+?)) )?-> ([a-z]+?)$");

            if (match.Groups[1].Success) //left side is a wire name
            {
                LeftInputWire = GetExistingWireOrNew(match.Groups[1].Value, wires);
            }

            if (match.Groups[2].Success) //left side is number (raw value)
            {
                LeftInputValue = match.Groups[2].Value.Parse<ushort>();
            }

            if (match.Groups[3].Success) //has an operator (missing it would be straight value assignment)
            {
                switch (match.Groups[3].Value)
                {
                    case "AND":
                        Operation = new AndGate();
                        break;
                    case "OR":
                        Operation = new OrGate();
                        break;
                    case "LSHIFT":
                        Operation = new LeftShiftGate();
                        break;
                    case "RSHIFT":
                        Operation = new RightShftGate();
                        break;
                    case "NOT":
                        Operation = new NotGate();
                        break;
                }
            }

            if (match.Groups[4].Success) //right side is wire name
            {
                RightInputWire = GetExistingWireOrNew(match.Groups[4].Value, wires);
            }

            if (match.Groups[5].Success) //right side is number
            {
                RightInputValue = match.Groups[5].Value.Parse<ushort>();
            }

            //group 6 needs to exist
            OutputWire = GetExistingWireOrNew(match.Groups[6].Value, wires);
        }

        private Wire GetExistingWireOrNew(string wireName, List<Wire> wires)
        {
            var existing = wires.SingleOrDefault(x => x.Name == wireName);

            if (existing != null)
                return existing;

            var newWire = new Wire { Name = wireName };
            wires.Add(newWire);

            return newWire;
        }
    }

    public class Wire
    {
        public string Name { get; set; }
        public ushort Value { get; set; }

        public override string ToString()
        {
            return $"{Name} | {Value}";
        }
    }

    public abstract class Gate
    {
        public abstract ushort CalculateOutputWireSignal(ushort? left, ushort? right);
    }

    public class AndGate : Gate
    {
        public override ushort CalculateOutputWireSignal(ushort? left, ushort? right) => (ushort)(left.Value & right.Value);
    }

    public class OrGate : Gate
    {
        public override ushort CalculateOutputWireSignal(ushort? left, ushort? right) => (ushort)(left.Value | right.Value);
    }

    public class LeftShiftGate : Gate
    {
        public override ushort CalculateOutputWireSignal(ushort? left, ushort? right) => (ushort)(left.Value << right.Value);
    }

    public class RightShftGate : Gate
    {
        public override ushort CalculateOutputWireSignal(ushort? left, ushort? right) => (ushort)(left.Value >> right.Value);
    }

    public class NotGate : Gate
    {
        public override ushort CalculateOutputWireSignal(ushort? left, ushort? right) => (ushort)~right.Value;
    }


}
