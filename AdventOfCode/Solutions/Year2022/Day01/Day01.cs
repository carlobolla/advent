
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day01 : ASolution
{
    private readonly string[] _lines;
    public Day01() : base(01, 2022, "Calorie Counting")
    {
        _lines = Input.Split(new []{ "\n\n" }, StringSplitOptions.None).ToArray();
    }

    private static IEnumerable<int> GetElves(IEnumerable<string> lines)
    {
        return lines.Select(elf => elf.SplitByNewline().Sum(int.Parse));
    }

    protected override string SolvePartOne()
    {
        var elves = GetElves(_lines);
        return elves.Max().ToString();
    }

    protected override string SolvePartTwo()
    {
        var elves = GetElves(_lines).ToList();
        return elves.OrderByDescending(e => e).Take(3).Sum().ToString();
    }
}
