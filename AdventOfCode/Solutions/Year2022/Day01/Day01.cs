
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day01 : ASolution
{
    private readonly string[] _lines;
    public Day01() : base(01, 2022, "Calorie Counting")
    {
        _lines = Input.Split(new []{ "\r", "\n", "\r\n" }, StringSplitOptions.None).ToArray();
    }

    private List<int> GetElves(string[] lines)
    {
        List<int> elves = new();
        int elf = 0;
        foreach (var line in lines)
        {
            if (line != "")
                elf += int.Parse(line);
            else
            {
                elves.Add(elf);
                elf = 0;
            }
        }
        return elves;
    }

    protected override string SolvePartOne()
    {
        List<int> elves = GetElves(_lines);
        return elves.Max().ToString();
    }

    protected override string SolvePartTwo()
    {
        List<int> elves = GetElves(_lines);
        elves.Sort((a, b) => b - a);
        return elves.Take(3).Sum().ToString();
    }
}
