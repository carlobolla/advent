using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2021;

internal class Day17 : ASolution
{
    public readonly record struct Point(int X, int Y)
    {
        public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);
    };
    private readonly record struct Rect(Point Start, Point End);
    private static bool IsHit(Point point, Rect rect)
    {
        return point.X >= rect.Start.X && point.X <= rect.End.X && point.Y >= rect.Start.Y && point.Y <= rect.End.Y;
    }
    private static IEnumerable<Point> FindVectors(Rect rect)
    {
        int lowerY = Math.Abs(Math.Min(rect.End.Y, rect.Start.Y)) * -1;
        int upperY = Math.Abs(Math.Min(rect.End.Y, rect.Start.Y));
        var vectors = Enumerable.Range(0, Math.Max(rect.End.X, rect.Start.X)+1)
            .SelectMany(_ => Enumerable.Range(lowerY, upperY - lowerY), (a, b) => new Point(a, b)).Select(x => x);
        return vectors.Where(v => CanHit(new Point(0, 0), v, rect));
    }
    private static bool CanHit(Point point, Point vector, Rect rect)
    {
        if (point.X > rect.End.X || point.Y < rect.Start.Y)
            return false;
        return IsHit(point, rect) || CanHit(point + vector, new Point(Math.Max(vector.X - 1, 0), vector.Y - 1), rect);
    }
    private readonly Rect _rect;
    public Day17() : base(17, 2021, "Trick Shot")
    {
        var groups = Regex.Match(Input, @"^target area: x=((-?\d+)..(-?\d+)), y=((-?\d+)..(-?\d+))$").Groups;
        Tuple<Point, Point> points = new(
            new Point(Convert.ToInt32(groups[2].Value), Convert.ToInt32(groups[5].Value)),
            new Point(Convert.ToInt32(groups[3].Value), Convert.ToInt32(groups[6].Value))
        );
        _rect = points.Item1.X > points.Item2.X ? new Rect(points.Item2, points.Item1) : new Rect(points.Item1, points.Item2);
    }

    protected override string SolvePartOne()
    {
        int minY = Math.Min(_rect.Start.Y, _rect.End.Y);
        return (minY * (1 + minY) / 2).ToString();
    }

    protected override string SolvePartTwo()
    {
        return FindVectors(_rect).Count().ToString();
    }
    
}