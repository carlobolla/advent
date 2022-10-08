using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021;

internal class Day14 : ASolution
{
    private readonly string _polymer;
    private readonly Dictionary<string, string> _rules = new();
    public Day14() : base(14, 2021, "")
    {
        string[] lines = Input.SplitByNewline();
        _polymer = lines[0];
        foreach (string line in lines[1..])
        {
            string[] splits = line.Split(" -> ");
            _rules[splits[0]] = splits[1];
        }
    }
    //I admit cheating for this one. We cannot operate on a string, otherwise in phase two we get to a Stack Overflow in like 3 steps.
    //So, we simulate stuff with Dictionaries keeping count of the letters and pairs.
    private string DoSteps(int steps)
    {
        Dictionary<string, ulong> polymerPairs = _rules.ToDictionary(r => r.Key, r => 0UL);
        Dictionary<char, ulong> polymers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToDictionary(x => x, x => 0UL);
        for (int i = 0; i < _polymer.Length; i++)
        {
            polymers[_polymer[i]]++;
            if (i + 1 < _polymer.Length && _rules.TryGetValue($"{_polymer[i]}{_polymer[i + 1]}", out string rule))
            {
                polymerPairs[$"{_polymer[i]}{_polymer[i + 1]}"]++;
            }
        }
        for (int i = 0; i < steps; i++)
        {
            polymerPairs = SimulatePolymerize(polymerPairs, ref polymers);
        }
        return (polymers.Max(x => x.Value) - polymers.Where(x => x.Value > 0).Min(x => x.Value)).ToString();
    }

    private Dictionary<string, ulong> SimulatePolymerize(Dictionary<string, ulong> polymerPairs, ref Dictionary<char, ulong> polymers)
    {
        Dictionary<string, ulong> newPolymerPairs = new(polymerPairs);
        foreach (KeyValuePair<string, string> rule in _rules)
        {
            if (polymerPairs.TryGetValue(rule.Key, out ulong count))
            {
                newPolymerPairs[rule.Key] = newPolymerPairs[rule.Key] - count;
                newPolymerPairs[$"{rule.Key[0]}{rule.Value}"] = newPolymerPairs[$"{rule.Key[0]}{rule.Value}"] + count;
                newPolymerPairs[$"{rule.Value}{rule.Key[1]}"] = newPolymerPairs[$"{rule.Value}{rule.Key[1]}"] + count;
                polymers[rule.Value[0]] = polymers[rule.Value[0]] + count;
            }
        }
        return newPolymerPairs;
    }
    protected override string SolvePartOne()
    {
        return DoSteps(10);
    }

    protected override string SolvePartTwo()
    {
        return DoSteps(40);
    }
}