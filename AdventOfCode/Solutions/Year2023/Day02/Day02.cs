using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023
{
    internal class Day02 : ASolution
    {
        private readonly List<string> lines;
        private readonly Regex gameRegex = new(@"Game (\d+)");
        private readonly Regex pullRegex = new(@"(?:(?:\d+) (?:blue|red|green)(?:, |)?)+");
        private readonly Regex redRegex = new(@"(\d+) red");
        private readonly Regex greenRegex = new(@"(\d+) green");
        private readonly Regex blueRegex = new(@"(\d+) blue");

        public Day02() : base(02, 2023, "Cube Conundrum")
        {
            lines = Input.SplitByNewline().ToList();
        }

        private class Game
        {
            public int Id { get; init; }
            public List<Pull> Pulls { get; init; } = new();
        }

        private class Pull
        {
            public int Red { get; init; }
            public int Green { get; init; }
            public int Blue { get; init; }
        }

        private Game MakeGame(string input)
        {
            return new Game()
            {
                Id = int.Parse(gameRegex.Match(input).Groups[1].Value),
                Pulls = pullRegex.Matches(input).Select(m =>
                    new Pull()
                    {
                        Red = int.TryParse(redRegex.Match(m.Value).Groups[1].Value, out var reds) ? reds : 0,
                        Green = int.TryParse(greenRegex.Match(m.Value).Groups[1].Value, out var greens) ? greens : 0,
                        Blue = int.TryParse(blueRegex.Match(m.Value).Groups[1].Value, out var blues) ? blues : 0
                    }).ToList()
            };
        }

        protected override string SolvePartOne()
        {
            var games = lines.Select(MakeGame).ToList();
            return games.Where(g =>
                g.Pulls.All(p => p.Red <= 12) &&
                g.Pulls.All(p => p.Green <= 13) &&
                g.Pulls.All(p => p.Blue <= 14)
            ).Sum(g => g.Id).ToString();
        }

        protected override string SolvePartTwo()
        {
            var games = lines.Select(MakeGame).ToList();
            return games.Select(g =>
                g.Pulls.Max(p => p.Red) * g.Pulls.Max(p => p.Green) * g.Pulls.Max(p => p.Blue)
            ).Sum().ToString();
        }
    }
}