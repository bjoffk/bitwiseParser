using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class BitwiseParser
{
    static void Main(string[] args)
    {

        var parser = new Parser();
        
        // Read and return string array of all lines in the text file. 
        var lines = File.ReadAllLines("input.txt");

        // Read the input file and break down each instruction to each wire.
        foreach (var line in lines)
        {
            parser.AddInstruction(line);
        }

        // Calculate and output the signal on wire a
        Console.WriteLine($"'a': {parser.GetSignal("a")}");
    }
}

public class Parser
{
    public Parser()
    {
        
    }
    // Constructor used for UnitTest
    public Parser(Dictionary<string, string> instructions)
    {
        this.instructions = instructions;
    }

    // Stores the signals for the wires
    private Dictionary<string, ushort> wireSignals = new Dictionary<string, ushort>();
    // Stores the instructions for each wire
    private Dictionary<string, string> instructions = new Dictionary<string, string>();

    public void AddInstruction(string instruction)
    {
        // Split string into parts
        string[] parts = instruction.Split(" -> ");
        // 1: Wire  0: Signal given
        instructions[parts[1]] = parts[0];
    }

    public ushort GetSignal(string wire)
    {
        // If the wire is a direct value
        if (ushort.TryParse(wire, out ushort value))
        {
            // Return the value
            return value;
        }

        // If the signal for the wire is already calculated, such as wire 'b' 
        if (wireSignals.ContainsKey(wire))
        {
            // Return it
            return wireSignals[wire];
        }

        // If the wire does not exist
        if (!instructions.ContainsKey(wire))
        {
            throw new Exception($"No instruction for wire: {wire}");
        }

        string instruction = instructions[wire];
        string[] parts = instruction.Split(' ');
        ushort result;

        switch (parts.Length)
        {
            case 1:
                // Direct assignment e.g. "1234 -> x" or "y -> z"
                result = GetSignal(parts[0]);
                break;

            case 2:
                // NOT operation e.g. "NOT x -> h". ~ bitwise NOT operator
                result = (ushort)~GetSignal(parts[1]);
                break;

            case 3:
                // Binary operation e.g. "x AND y -> z", "x OR y -> z", and so on
                string operand1 = parts[0];
                string operand2 = parts[2];
                string operation = parts[1];

                ushort signal1 = GetSignal(operand1);
                ushort signal2 = GetSignal(operand2);

                result = operation switch
                {
                    "AND" => (ushort)(signal1 & signal2),
                    "OR" => (ushort)(signal1 | signal2),
                    "LSHIFT" => (ushort)(signal1 << int.Parse(operand2)),
                    "RSHIFT" => (ushort)(signal1 >> int.Parse(operand2)),
                    _ => throw new Exception($"Unknown operation: {operation}")
                };
                break;

            default:
                throw new Exception($"Invalid instruction: {instruction}");
        }

        // As each instruction is processed, update the corresponding wire's value based on its dependencies.
        wireSignals[wire] = result;
        // Once all instructions are processed, the value of wire a can be returned.
        return result;
    }
}