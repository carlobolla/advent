using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day12 : ASolution
{
    private readonly char[] _elevationPriorities = "SabcdefghijklmnopqrstuvwxyzE".ToCharArray();
    private record Point(int X, int Y, int Elevation);
    private readonly List<Point> _map;

    public Day12() : base(12, 2022, "Hill Climbing Algorithm")
    {
        _map = Input.SplitByNewline()
            .SelectMany((l, i) => l.Select((c, i2) => new Point(i2, i, Array.IndexOf(_elevationPriorities, c))))
            .ToList();
    }

    private IEnumerable<Point> GetNeighbours(Point p)
    {
        return _map.Where(x => x.X == p.X && x.Y == p.Y + 1 ||
                               x.X == p.X && x.Y == p.Y - 1 ||
                               x.X == p.X + 1 && x.Y == p.Y ||
                               x.X == p.X - 1 && x.Y == p.Y)
            .Where(x => p.Elevation <= x.Elevation + 1)
            .ToList();
    }

    private Dictionary<Point, int> GetDistanceMap()
    {
        //Breadth-first search starting from goal for every point on the map
        var goal = _map.First(x => x.Elevation == 27);
        var queue = new Queue<Point>();
        var visited = new Dictionary<Point, int>();
        queue.Enqueue(goal);
        visited.Add(queue.Peek(), 0);
        while (queue.Any())
        {
            var current = queue.Dequeue();
            foreach (var neighbour in GetNeighbours(current).Where(neighbour => !visited.ContainsKey(neighbour)))
            {
                queue.Enqueue(neighbour);
                visited.Add(neighbour, visited[current] + 1);
            }
        }

        return visited;
    }

    private int MinDistanceTo(int elevation)
    {
        return GetDistanceMap()
            .Where(x => x.Key.Elevation == elevation)
            .Select(x => x.Value)
            .Min() + 1;
    }

    protected override string SolvePartOne()
    {
        return MinDistanceTo(0).ToString();
    }

    protected override string SolvePartTwo()
    {
        return MinDistanceTo(1).ToString();
    }
}
