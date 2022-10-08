using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day09 : ASolution
{
    private readonly string[] _lines;
    private readonly List<Point> _lowPoints;
    public Day09() : base(09, 2021, "")
    {
        _lines = Input.SplitByNewline();
        _lowPoints = new List<Point>();
    }
    public record Point(int X, int Y);

    private IEnumerable<Point> Neighbors(Point point)
    {
        return new[] {
            point with {Y = point.Y +1},
            point with {X = point.X +1},
            point with {Y = point.Y -1},
            point with {X = point.X -1}
        };
    }

    private ImmutableDictionary<Point, int> GetMap()
    {
        var map = Input.Split("\n");
        return Enumerable.Range(0, map[0].Length).SelectMany( i => Enumerable.Range(0, map.Length), (x, y) => (x, y)).Select(i => new KeyValuePair<Point, int>(new Point(i.x, i.y), Convert.ToInt32(Char.GetNumericValue(map[i.y][i.x])))).ToImmutableDictionary();
    }

    public IEnumerable<Point> GetLowPoints(ImmutableDictionary<Point, int> map)
    {
        return map.Where(point => Neighbors(point.Key).All(neighbor => map[point.Key] < map.GetValueOrDefault(neighbor, 9))).Select(i => i.Key);
    }

    public int BasinSize(ImmutableDictionary<Point, int> map, Point point)
    {
        // flood fill algorithm
        var filled = new HashSet<Point> { point };
        var queue = new Queue<Point>(filled);

        while (queue.Any())
        {
            foreach (var neighbor in Neighbors(queue.Dequeue()).Except(filled))
            {
                if (map.GetValueOrDefault(neighbor, 9) != 9)
                {
                    queue.Enqueue(neighbor);
                    filled.Add(neighbor);
                }
            }
        }
        return filled.Count;
    }

    protected override string SolvePartOne()
    {
        ImmutableDictionary<Point, int> map = GetMap();
        return GetLowPoints(map).Count().ToString();
    }
    protected override string SolvePartTwo()
    {
        ImmutableDictionary<Point, int> map = GetMap();

        return GetLowPoints(map)
            .Select(p => BasinSize(map, p))
            .OrderByDescending(basinSize => basinSize)
            .Take(3)
            .Aggregate(1, (m, basinSize) => m * basinSize).ToString();
    }
}