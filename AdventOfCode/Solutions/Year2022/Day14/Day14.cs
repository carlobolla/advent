using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

public record Point(int X, int Y);
public static class PointExtensions
{
    public static IEnumerable<Point> LineTo(this Point start, Point end)
    {
        if(start.X != end.X && start.Y != end.Y)
            throw new ArgumentException("Points must be on the same line");
        if (start.X == end.X)
            return Enumerable.Range(Math.Min(start.Y, end.Y), Math.Abs(start.Y - end.Y) + 1)
                .Select(y => start with { Y = y });
        return Enumerable.Range(Math.Min(start.X, end.X), Math.Abs(start.X - end.X) + 1)
            .Select(x => start with { X = x });
    }
}

internal class Day14 : ASolution
{
    private int _voidLimit;
    public Day14() : base(14, 2022, "Regolith Reservoir")
    {
    }

    private Dictionary<Point, int> GetRocks()
    {
        var cave = Input.SplitByNewline()
            .Select(line =>
                {
                    var vertexes = line.Split(" -> ")
                        .Select(g => g.Split(","))
                            .Select(c => new Point(int.Parse(c[0]), int.Parse(c[1])))
                        .ToList();
                    List<Point> points = new();
                    for (int i = 0; i < vertexes.Count - 1; i++)
                        points.AddRange(vertexes[i].LineTo(vertexes[i + 1]));
                    return points;
                })
            .Aggregate(new Dictionary<Point, int>(), (dict, list) =>
                {
                    foreach (var point in list)
                        dict[point] = 0;
                    return dict;
                });
        _voidLimit = cave.Keys.Max(p => p.Y);
        return cave;
    }

    private bool SpawnSand(IDictionary<Point, int> cave, bool partTwo = false)
    {
        var sand = new Point(500, 0);
        while(sand.Y <= _voidLimit + (partTwo ? 2 : 0))
        {
            var previous = sand;
            if (!cave.ContainsKey(sand = sand with { Y = sand.Y + 1 })) continue;
            if (!cave.ContainsKey(sand = sand with { X = sand.X - 1 })) continue;
            if (!cave.ContainsKey(sand = sand with { X = sand.X + 2 })) continue;
            cave[previous] = 1;
            return previous != new Point(500,0);
        }
        if(!partTwo) return false;
        cave[sand] = 1;
        return true;
    }

    protected override string SolvePartOne()
    {
        var cave = GetRocks();
        while (SpawnSand(cave)) { }
        return cave.Count(p => p.Value == 1).ToString();
    }

    protected override string SolvePartTwo()
    {
        var cave = GetRocks();
        foreach (var i in Enumerable.Range(0, 10000)) // should suffice
            cave[new Point(i, _voidLimit + 2)] = 0;
        while (SpawnSand(cave, true)) { }
        return cave.Count(p => p.Value == 1).ToString();
    }
}
