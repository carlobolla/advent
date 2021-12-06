using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day01 : ASolution
    {
        readonly int[] lines;
        public Day01() : base(01, 2021, "")
        {
            lines = Input.SplitByNewline().ToIntArray();
        }

        protected override string SolvePartOne()
        {
            return lines.Select((s, i) => new { i, s }).Where(t => t.i!=0 && t.s > lines[t.i - 1]).Count().ToString();
        }

        protected override string SolvePartTwo()
        {
            return lines.Select((s, i) => new { i, s }).Where(t => t.i +4 <= lines.Count() && lines[t.i..(t.i+3)].Sum() < lines[(t.i+1)..(t.i + 4)].Sum()).Count().ToString();
        }
    }
}