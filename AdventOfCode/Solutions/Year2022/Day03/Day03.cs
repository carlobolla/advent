
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day03 : ASolution
{
    private readonly char[] _priority = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private readonly List<string> _lines;

    public Day03() : base(03, 2022, "Rucksack Reorganization")
    {
        _lines = Input.SplitByNewline().ToList();
    }

    protected override string SolvePartOne()
    {
        return _lines.Sum(line => Array.IndexOf(_priority, line[..(line.Length / 2)].First(x => line[(line.Length / 2)..].Contains(x))) +1).ToString();
    }

    protected override string SolvePartTwo()
    {
        var groups = _lines.GroupBy(x => _lines.IndexOf(x) / 3);
        int prioritySum = groups.Select(group => group.ToArray())
            .Select(array => Array.IndexOf(_priority, array[0].Intersect(array[1]).Intersect(array[2]).First()) +1)
            .Sum();

        return prioritySum.ToString();
    }
}
