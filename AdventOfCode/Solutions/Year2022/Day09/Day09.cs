using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day09 : ASolution
{
    private record Point(int X, int Y);
    private readonly string[] _lines; 
    public Day09() : base(09, 2022, "")
    {
        _lines = Input.SplitByNewline();
    }
    
    private static Point MoveHead(Point p, string c)
    {
        return c switch
        {
            "U" => p with {Y = p.Y + 1},
            "D" => p with {Y = p.Y - 1},
            "L" => p with {X = p.X - 1},
            "R" => p with {X = p.X + 1},
            _ => throw new Exception("Invalid direction")
        };
    }
    
    private static bool IsAdjacent(Point p1, Point p2)
    {
        return Math.Abs(p1.X - p2.X) <= 1 && Math.Abs(p1.Y - p2.Y) <= 1;
    }

    private static Point MoveTail(Point t, Point h)
    {
        if (IsAdjacent(t, h)) return t;
        (int x, int y) = (t.X, t.Y);
        if (t.X < h.X) x++;
        if (t.X > h.X) x--;
        if (t.Y < h.Y) y++;
        if (t.Y > h.Y) y--;
        return new Point(x, y);
    }

    protected override string SolvePartOne()
    {
        HashSet<Point> visited = new();
        var (h, t) = (new Point(0, 0), new Point(0,0));
        foreach (string line in _lines)
        {
            string[] instructions = line.Split(' ');
            for(int i = 0; i < int.Parse(instructions[1]); i++)
            {
                h = MoveHead(h, instructions[0]);
                t = MoveTail(t, h);
                visited.Add(t);
            }
        }

        return visited.Count.ToString();
    }

    protected override string SolvePartTwo()
    {
        HashSet<Point> visited = new();
        List<Point> points = new(Enumerable.Repeat(new Point(0, 0), 10));
        foreach (string line in _lines)
        {
            string[] instructions = line.Split(' ');
            for(int i = 0; i < int.Parse(instructions[1]); i++)
            {
                points[0] = MoveHead(points[0], instructions[0]);
                for (int j = 1; j < points.Count; j++)
                {
                    points[j] = MoveTail(points[j], points[j - 1]);
                }
                visited.Add(points[^1]);
            }
        }
        return visited.Count.ToString();
    }
}
