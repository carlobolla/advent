using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2022;

internal class Day05 : ASolution
{
    private static List<Stack<char>> GetStacks(IReadOnlyList<string> lines)
    {
        List<Stack<char>> stacks = new();
        int stacksNumber = (lines[0].Length + 1) / 4;
        for (int i = 0; i < stacksNumber; i++)
        {
            Queue<char> stack = new(); 
            foreach (var t in lines)
            {
                var match = Regex.Match((t + ' ').Substring(i * 4, 4), @"^\[([A-Z])] ?$");
                if(match.Success)
                    stack.Enqueue(match.Captures[0].Value[1]);
            }
            stacks.Add(new Stack<char>(stack.Reverse()));
        }

        return stacks;
    }
    
    private readonly string[] _lines;
    public Day05() : base(05, 2022, "Supply Stacks")
    {
        _lines = Input.SplitByNewlineKeepEmpty();
    }

    protected override string SolvePartOne()
    {
        var stacks = GetStacks(_lines[..Array.IndexOf(_lines, "")]);
        var instructions = _lines[(Array.IndexOf(_lines, "") + 1)..];
        foreach (string instruction in instructions)
        {
            var match = Regex.Match(instruction, @"^move ([0-9]+) from ([0-9]+) to ([0-9]+)$");
            if (!match.Success) continue;
            int quantity = int.Parse(match.Groups[1].Value);
            for (int i = 0; i < quantity; i++)
            {
                stacks[int.Parse(match.Groups[3].Value) - 1].Push(stacks[int.Parse(match.Groups[2].Value) - 1].Pop());
            }
        }
        return string.Join("", stacks.Select(x => x.Pop()));
    }

    protected override string SolvePartTwo()
    {
        var stacks = GetStacks(_lines[..Array.IndexOf(_lines, "")]);
        var instructions = _lines[(Array.IndexOf(_lines, "") + 1)..];
        foreach (string instruction in instructions)
        {
            var match = Regex.Match(instruction, @"^move ([0-9]+) from ([0-9]+) to ([0-9]+)$");
            if (!match.Success) continue;
            int quantity = int.Parse(match.Groups[1].Value);
            Stack<char> temp = new();
            for (int i = 0; i < quantity; i++)
            {
                temp.Push(stacks[int.Parse(match.Groups[2].Value) - 1].Pop());
            }
            while (temp.TryPop(out char c))
            { 
                stacks[int.Parse(match.Groups[3].Value) - 1].Push(c);
            }
        }
        return string.Join("", stacks.Select(x => x.Pop()));
    }
}
