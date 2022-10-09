using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;
internal class Day15 : ASolution
{
    public Day15() : base(15, 2021, "Chiton")
    {}

    protected override string SolvePartOne()
    {
        var grid = GetRiskLevelMap(Input);
        return Dijkstra(grid);
    }

    protected override string SolvePartTwo()
    {
        var grid = ScaleUp(GetRiskLevelMap(Input));
        return Dijkstra(grid);
    }
    public string Dijkstra(Dictionary<Point, int> grid){
        // Dijkstra's algorithm
        var start = new Point(0, 0);
        var end = new Point(grid.Keys.MaxBy(p => p.X).X, grid.Keys.MaxBy(p => p.Y).Y);
        var queue = new PriorityQueue<Point, int>();
        var distances = new Dictionary<Point, int>();
        distances[start] = 0;
        queue.Enqueue(start, 0);

        while (true) {
            var p = queue.Dequeue();

            if (p.Equals(end)){
                break;
            }

            foreach (var n in GetNeighbors(p)) {
                if (grid.ContainsKey(n)) {
                    var totalRiskThroughP = distances[p] + grid[n];
                    if (totalRiskThroughP < distances.GetValueOrDefault(n, int.MaxValue)) {
                        distances[n] = totalRiskThroughP;
                        queue.Enqueue(n, totalRiskThroughP);
                    }
                }   
            }
        }
        return distances[end].ToString();
    }

    Dictionary<Point, int> GetRiskLevelMap(string input) {
        var map = input.SplitByNewline();
        return new Dictionary<Point, int>(map.SelectMany((r, y) => r.Select((c, x) => new KeyValuePair<Point, int>(new Point(x, y), (int)char.GetNumericValue(c)))));
    }
    public List<Point> GetNeighbors(Point point){
        var neighbours = new List<Point>();
        neighbours.Add(new Point(point.X + 1, point.Y));
        neighbours.Add(new Point(point.X - 1, point.Y));
        neighbours.Add(new Point(point.X, point.Y + 1));
        neighbours.Add(new Point(point.X, point.Y - 1));
        return neighbours;
    }

    Dictionary<Point, int> ScaleUp(Dictionary<Point, int> map) {
        var (ccol, crow) = (map.Keys.MaxBy(p => p.X).X + 1, map.Keys.MaxBy(p => p.Y).Y + 1);

        var res = new Dictionary<Point, int>(
            from y in Enumerable.Range(0, crow * 5)
            from x in Enumerable.Range(0, ccol * 5)

            let tileY = y % crow
            let tileX = x % ccol
            let tileRiskLevel = map[new Point(tileX, tileY)]

            let tileDistance = (y / crow) + (x / ccol)

            let riskLevel = (tileRiskLevel + tileDistance - 1) % 9 + 1
            select new KeyValuePair<Point, int>(new Point(x, y), riskLevel)
        );

        return res;
    }
}