using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023
{
    internal class Day04 : ASolution
    {
        private readonly string[] lines;
        //this also captures garbage in position 0. I can't be bothered to fix it, so i'll just skip position 0 later
        private readonly Regex numberRegex = new("([ ][ 1-9][0-9])");
        private readonly Regex cardIdRegex = new(@"Card *([\d]*):");

        public Day04() : base(04, 2023, "Scratchcards")
        {
            lines = Input.SplitByNewline();
        }

        private class Card
        {
            public int Id { get; init; }
            public List<int> WinningNumbers { get; init; } = new();
            public List<int> CardNumbers { get; init; } = new();
        }

        private static int getScore(Card card, bool summed)
        {
            var winning = card.WinningNumbers.Intersect(card.CardNumbers).Count();
            if(summed)
                return winning == 0 ? 0 : (int)Math.Pow(2, winning - 1);
            return winning;
        }

        private List<Card> GetCards()
        {
            return lines.Select(l =>
            {
                var matches = numberRegex.Matches(l);
                return new Card()
                {
                    Id = int.Parse(cardIdRegex.Match(l).Groups[1].Value),
                    CardNumbers = matches.Skip(11).Select(m => int.Parse(m.Value)).ToList(),
                    WinningNumbers = matches.Skip(1).Take(10).Select(m => int.Parse(m.Value)).ToList(),
                };
            }).ToList();
        }
        
        protected override string SolvePartOne()
        {
            return GetCards()
                .Sum(c => getScore(c, true))
                .ToString();
        }

        protected override string SolvePartTwo()
        {
            var cards = GetCards();
            var counters = Enumerable.Range(1, cards.Count).ToDictionary(i => i, _ => 1);
            cards.ForEach(c =>
            {
                foreach (var i in Enumerable.Range(c.Id, getScore(c, false)))
                {
                    counters[i+1] += counters[c.Id];
                }
            });
            return counters.Values.Sum().ToString();
        }
    }
}