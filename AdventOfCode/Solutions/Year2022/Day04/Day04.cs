
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day04 : ASolution
{
    private readonly string[] _lines;
    public Day04() : base(04, 2022, "Camp Cleanup")
    {
        _lines = Input.SplitByNewline();
    }

    private static IEnumerable<IEnumerable<int>[]> GetRanges(IEnumerable<string[]> pairs)
    {
        return pairs.Select(pair =>
        {
            int[][] ranges = pair.Select(x => x.Split('-').Select(int.Parse).ToArray()).Select(x => new[] { x[0], x[1] }).ToArray();
            IEnumerable<int> range1 = Enumerable.Range(ranges[0][0], ranges[0][1] - ranges[0][0] +1).ToArray();
            IEnumerable<int> range2 = Enumerable.Range(ranges[1][0], ranges[1][1] - ranges[1][0] +1).ToArray();
            return new[] { range1, range2 };
        });
    }

    protected override string SolvePartOne()
    {
        var pairs = _lines.Select(x => x.Split(','));
        return GetRanges(pairs).Count(ranges => ranges[0].All(x => ranges[1].Contains(x)) || ranges[1].All(x => ranges[0].Contains(x))).ToString();
    }

    protected override string SolvePartTwo()
    {
        var pairs = _lines.Select(x => x.Split(',')).ToList();
        return GetRanges(pairs).Count(ranges => ranges[0].Any(x => ranges[1].Contains(x)) || ranges[1].Any(x => ranges[0].Contains(x))).ToString();
    }
}
