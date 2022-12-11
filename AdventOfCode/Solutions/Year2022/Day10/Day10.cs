using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day10 : ASolution
{
    private readonly string[] _lines;
    private readonly int[] _splits;
    public Day10() : base(10, 2022, "Cathode-Ray Tube")
    {
        _lines = Input.SplitByNewline();
        _splits = new[] { 20, 60, 100, 140, 180, 220 };
    }

    protected override string SolvePartOne()
    {
        int cycle = 0;
        List<int> register = new(){1};
        foreach (string line in _lines)
        {
            var instruction = line.Split(" ");
            switch (instruction[0])
            {
                case "noop":
                    register.Add(register[cycle]);
                    cycle++;
                    break;
                case "addx":
                    register.Add(register[cycle]);
                    cycle++;
                    register.Add(register[cycle] + int.Parse(instruction[1]));
                    cycle++;
                    break;
            }
        }

        return _splits.Aggregate(0, (a, b) => a + b * register[b - 1]).ToString();
    }

    protected override string SolvePartTwo()
    {
        string result = "";
        int cycle = 0;
        List<int> register = new(){1};
        foreach (string line in _lines)
        {
            var instruction = line.Split(" ");
            switch (instruction[0])
            {
                case "noop":
                    result += Enumerable.Range(register[cycle] - 1, 3).Contains(cycle % 40) ? '#' : ' ';
                    register.Add(register[cycle]);
                    cycle++;
                    break;
                case "addx":
                    result += Enumerable.Range(register[cycle] - 1, 3).Contains(cycle % 40) ? '#' : ' ';
                    register.Add(register[cycle]);
                    cycle++;
                    result += Enumerable.Range(register[cycle] - 1, 3).Contains(cycle % 40) ? '#' : ' ';
                    register.Add(register[cycle] + int.Parse(instruction[1]));
                    cycle++;
                    break;
            }
        }

        return "\n" + string.Join("\n", _splits.Select(s => result[(s - 20)..(s + 20)]));
    }
}