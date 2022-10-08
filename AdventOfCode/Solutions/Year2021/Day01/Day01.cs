using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day01 : ASolution
{
    private readonly int[] _lines;
    public Day01() : base(01, 2021, "Sonar Sweep")
    {
        _lines = Input.SplitByNewline().ToIntArray();
    }

    protected override string SolvePartOne()
    {
        return _lines.Select((s, i) => new { i, s }).Count(t => t.i!=0 && t.s > _lines[t.i - 1]).ToString();
    }

    protected override string SolvePartTwo()
    {
        return _lines.Select((s, i) => new { i, s }).Count(t => t.i +4 <= _lines.Length && _lines[t.i..(t.i+3)].Sum() < _lines[(t.i+1)..(t.i + 4)].Sum()).ToString();
    }
}