using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day14 : ASolution
    {
        string polymer;
        Dictionary<string, string> rules = new();
        public Day14() : base(14, 2021, "")
        {
            string[] lines = Input.SplitByNewline();
            polymer = lines[0];
            foreach (string line in lines[1..])
            {
                string[] splits = line.Split(" -> ");
                rules[splits[0]] = splits[1];
            }
        }
        //I admit cheating for this one. We cannot operate on a string, otherwise in phase two we get to a Stack Overflow in like 3 steps.
        //So, we simulate stuff with Dictionaries keeping count of the letters and pairs.
        string DoSteps(int steps)
        {
            Dictionary<string, ulong> PolymerPairs = rules.ToDictionary(r => r.Key, r => 0UL);
            Dictionary<char, ulong> Polymers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToDictionary(x => x, x => 0UL);
            for (int i = 0; i < polymer.Length; i++)
            {
                Polymers[polymer[i]]++;
                if (i + 1 < polymer.Length && rules.TryGetValue($"{polymer[i]}{polymer[i + 1]}", out string rule))
                {
                    PolymerPairs[$"{polymer[i]}{polymer[i + 1]}"]++;
                }
            }
            for (int i = 0; i < steps; i++)
            {
                PolymerPairs = SimulatePolymerize(PolymerPairs, ref Polymers);
            }
            return (Polymers.Max(x => x.Value) - Polymers.Where(x => x.Value > 0).Min(x => x.Value)).ToString();
        }
        Dictionary<string, ulong> SimulatePolymerize(Dictionary<string, ulong> PolymerPairs, ref Dictionary<char, ulong> Polymers)
        {
            Dictionary<string, ulong> NewPolymerPairs = new(PolymerPairs);
            foreach (KeyValuePair<string, string> rule in rules)
            {
                if (PolymerPairs.TryGetValue(rule.Key, out ulong count))
                {
                    NewPolymerPairs[rule.Key] = NewPolymerPairs[rule.Key] - count;
                    NewPolymerPairs[$"{rule.Key[0]}{rule.Value}"] = NewPolymerPairs[$"{rule.Key[0]}{rule.Value}"] + count;
                    NewPolymerPairs[$"{rule.Value}{rule.Key[1]}"] = NewPolymerPairs[$"{rule.Value}{rule.Key[1]}"] + count;
                    Polymers[rule.Value[0]] = Polymers[rule.Value[0]] + count;
                }
            }
            return NewPolymerPairs;
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
}
