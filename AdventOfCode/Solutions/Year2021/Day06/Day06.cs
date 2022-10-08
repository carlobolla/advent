using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day06 : ASolution
{
    private readonly string[] _lines;
    private long[] _feeshByTimer = new long[9];

    private void AdvanceDays(long[] feeshByDays, int days)
    {
        for (int d = 0; d < days; d++)
        {
            feeshByDays[(d + 7) % 9] += feeshByDays[d % 9];
        }
    }
    public Day06() : base(06, 2021, "Lanternfish")
    {
        _lines = Input.SplitByNewline();
    }

    protected override string SolvePartOne()
    {
        foreach(int i in _lines[0].Split(",").Select(i => int.Parse(i)))
        {
            _feeshByTimer[i]++;
        }
        AdvanceDays(_feeshByTimer, 80);
        return _feeshByTimer.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        _feeshByTimer = new long[9];
        foreach (int i in _lines[0].Split(",").Select(i => int.Parse(i)))
        {
            _feeshByTimer[i]++;
        }
        AdvanceDays(_feeshByTimer, 256);
        return _feeshByTimer.Sum().ToString();
    }
}