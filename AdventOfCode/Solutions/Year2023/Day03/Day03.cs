using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023
{
    internal class Day03 : ASolution
    {
        private readonly string[] lines;
        private readonly Regex numbersRegex = new(@"\d+");
        private readonly Regex symbolsRegex = new("[^.0-9]");
        private readonly Regex gearsRegex = new(@"\*");

        public Day03() : base(03, 2023, "Gear Ratios")
        {
            lines = Input.SplitByNewline();
        }
        
        private record PartNumber(string Text, int rowIndex, int startCol) {
            public int Int => int.Parse(Text);
        }

        private List<PartNumber> ParseInput(Regex rx)
        {
            var parts = new List<PartNumber>();
            foreach (var line in lines.Select((content, index) => new {index, content}))
            {
                parts.AddRange(rx.Matches(line.content).Select(m => new PartNumber(m.Value, line.index, m.Index)));
            }

            return parts;
        }

        private static bool AreAdjacent(PartNumber part1, PartNumber part2) =>
            Math.Abs(part2.rowIndex - part1.rowIndex) <= 1 &&
            part1.startCol <= part2.startCol + part2.Text.Length &&
            part2.startCol <= part1.startCol + part1.Text.Length;

        
        protected override string SolvePartOne()
        {
            var symbols = ParseInput(symbolsRegex);
            var numbers = ParseInput(numbersRegex);
            return numbers.Where(n => symbols.Any(s => AreAdjacent(s, n))).Sum(n => n.Int).ToString();
        }

        protected override string SolvePartTwo()
        {
            var gears = ParseInput(gearsRegex);
            var numbers = ParseInput(numbersRegex);
            return gears.Select(g => new { g, neighbours = numbers.Where(n => AreAdjacent(n, g)).Select(n => n.Int) })
                .Where(t => t.neighbours.Count() == 2)
                .Select(t => t.neighbours.First() * t.neighbours.Last()).Sum().ToString();
        }
    }
}