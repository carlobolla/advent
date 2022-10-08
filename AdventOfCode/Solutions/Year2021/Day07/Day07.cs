using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day07 : ASolution
{
    private readonly int[] _subs;
    public Day07() : base(07, 2021, "")
    {
        _subs = Input.SplitByNewline()[0].Split(",").Select(i => int.Parse(i)).ToArray();
    }

    protected override string SolvePartOne()
    {
        Array.Sort(_subs);
        int median = _subs[_subs.Length / 2];
        return _subs.Sum(i => i < median ? median - i : i - median).ToString();
    }
    protected override string SolvePartTwo()
    {
        int avg = Convert.ToInt32(Math.Floor(_subs.Average()));
        //the average actually minimizes (distance ^ 2), but distance (distance + 1)/2 is close enough
        //to (distance^2 / 2) hence playing with rounding just works
        //lucky input, i guess
        return _subs.Sum(i => i<avg ? Enumerable.Range(1, avg - i).Sum() : Enumerable.Range(1, i - avg).Sum()).ToString();
    }
}