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
            var instructions = File.ReadAllLines("Input.txt").ToList();

            var part1Board = new CircuitBoard();

            foreach (var instruction in instructions)
            {
                part1Board.ApplyInstruction(instruction);
            }

            part1Board.CalculateWireValues();

            var part1Answer = part1Board.GetWireSignal("a");

            //----------

            var part2Board = new CircuitBoard();

            var existingBInstruction = instructions.Single(x => x.EndsWith("-> b"));
            instructions.Remove(existingBInstruction);

            foreach (var instruction in instructions)
            {
                part2Board.ApplyInstruction(instruction);
            }

            part2Board.ApplyInstruction("46065 -> b");

            part2Board.CalculateWireValues();

            var part2Answer = part2Board.GetWireSignal("a");
        }
    }

    public class CircuitBoard
    {
        private List<Wire> wires;
        private List<Instruction> instructions;

        public CircuitBoard()
        {
            wires = new List<Wire>();
            instructions = new List<Instruction>();
        }

        public void ApplyInstruction(string instruction)
        {
            var parsedInstruction = new Instruction(instruction, wires);
            instructions.Add(parsedInstruction);
        }

        public void CalculateWireValues()
        {
            foreach (var instruction in instructions)
            {
                CalculateWireSignal(instruction.OutputWire);
            }
        }

        private void CalculateWireSignal(Wire wire)
        {
            //if the wire already has a value set, stop here.
            if (wire.Value != null)
                return;

            //if not, we need to figure out this wire's value
            //to do that, we need to know where the source is, and calculate that if need be

            //to get the source, find the single instruction that has this wire as the output
            var sourceInstructionForWire = instructions.Single(x => x.OutputWire == wire);

            //if the instruction uses input wires, we need to resolve those wires first
            if (sourceInstructionForWire.LeftInputWire != null)
            {
                CalculateWireSignal(sourceInstructionForWire.LeftInputWire);
            }
            if (sourceInstructionForWire.RightInputWire != null)
            {
                CalculateWireSignal(sourceInstructionForWire.RightInputWire);
            }

            //if the instruction has an operator, run the operation and apply the value to the output wire
            if (sourceInstructionForWire.Operation != null)
            {
                var left = sourceInstructionForWire.LeftInputWire?.Value ?? sourceInstructionForWire.LeftInputValue;
                var right = sourceInstructionForWire.RightInputWire?.Value ?? sourceInstructionForWire.RightInputValue;

                wire.Value = sourceInstructionForWire.Operation.CalculateOutputWireSignal(left, right);
            }
            else if (sourceInstructionForWire.LeftInputWire != null) //else, if the instruction uses a single wire for assignment
            {
                wire.Value = sourceInstructionForWire.LeftInputWire.Value.Value;
            }
            else //else, this is a number assignment for the wire
            {
                wire.Value = sourceInstructionForWire.LeftInputValue.Value;
            }
        }

        public ushort GetWireSignal(string wireName)
        {
            return wires.Single(x => x.Name == wireName).Value.Value;
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
        public ushort? Value { get; set; }

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
