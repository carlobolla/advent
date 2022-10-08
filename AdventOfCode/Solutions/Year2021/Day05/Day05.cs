using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day05 : ASolution
{
    private readonly string[] _lines;
    private readonly List<Pipe> _pipes = new();
    public Day05() : base(05, 2021, "Hydrothermal Venture")
    {
        _lines = Input.SplitByNewline();
        foreach(string line in _lines)
        {
            string[] splits = line.Split(" -> ");
            int[] coordinates1 = splits[0].Split(",").Select(i => int.Parse(i)).ToArray();
            int[] coordinates2 = splits[1].Split(",").Select(i => int.Parse(i)).ToArray();
            Point point1 = new(coordinates1[0], coordinates1[1]);
            Point point2 = new(coordinates2[0], coordinates2[1]);
            _pipes.Add(new Pipe(point1, point2));
        }
    }

    protected override string SolvePartOne()
    {
        return _pipes.Where(i => i.Straight).SelectMany(i=>i.Path).GroupBy(i=>i).Where(i=>i.Count() >= 2).Count().ToString();   
    }

    protected override string SolvePartTwo()
    {
        return _pipes.SelectMany(i => i.Path).GroupBy(i => i).Where(i => i.Count() >= 2).Count().ToString();
    }
}