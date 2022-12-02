using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

public class Point{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public override string ToString()
    {
        return $"({this.X},{this.Y})";
    }
    public override bool Equals(object? obj)
    {
        return Equals((Point)obj!);
    }
    public bool Equals(Point obj)
    {
        return obj.X == this.X && obj.Y == this.Y;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
public class Pipe
{
    public Point StartingPoint { get; private set; }
    public Point EndingPoint { get; private set; }
    public List<Point> Path { get; private set; }
    public bool Straight { get; private set; }
    public Pipe(Point starting, Point ending)
    {
        StartingPoint = starting;
        EndingPoint = ending;
        Path = new List<Point>();
        if(StartingPoint.X == EndingPoint.X)
        {
            this.Straight = true;
            Path.Add(StartingPoint);
            foreach (int i in Enumerable.Range(1, Math.Max(StartingPoint.Y, EndingPoint.Y) - Math.Min(StartingPoint.Y, EndingPoint.Y) -1))
            {
                Path.Add(new Point(StartingPoint.X, Math.Min(StartingPoint.Y, EndingPoint.Y)+i));
            }
            Path.Add(EndingPoint);
        } else if (StartingPoint.Y == EndingPoint.Y)
        {
            Path.Add(StartingPoint);
            this.Straight = true;
            foreach (int i in Enumerable.Range(1, Math.Max(StartingPoint.X, EndingPoint.X) - Math.Min(StartingPoint.X, EndingPoint.X) -1))
            {
                Path.Add(new Point(Math.Min(StartingPoint.X, EndingPoint.X) + i, StartingPoint.Y));
            }
            Path.Add(EndingPoint);
        }
        else
        {
            Path.Add(StartingPoint);
            if((StartingPoint.X > EndingPoint.X && StartingPoint.Y > EndingPoint.Y) || (StartingPoint.X < EndingPoint.X && StartingPoint.Y < EndingPoint.Y))
            {
                foreach (int i in Enumerable.Range(1, Math.Max(StartingPoint.X, EndingPoint.X) - Math.Min(StartingPoint.X, EndingPoint.X) - 1))
                {
                    Path.Add(new Point(Math.Min(StartingPoint.X, EndingPoint.X) + i, Math.Min(StartingPoint.Y, EndingPoint.Y) + i));
                }
            } else
            {
                foreach (int i in Enumerable.Range(1, Math.Max(StartingPoint.X, EndingPoint.X) - Math.Min(StartingPoint.X, EndingPoint.X) - 1))
                {
                    Path.Add(new Point(Math.Min(StartingPoint.X, EndingPoint.X) + i, Math.Max(StartingPoint.Y, EndingPoint.Y) - i));
                }
            }
            Path.Add(EndingPoint);
        }
    }
    public override string ToString()
    {
        return $"Pipe from {StartingPoint} to {EndingPoint}";
    }
}