using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023
{
    internal class Day01 : ASolution
    {
        private readonly List<string> lines;
        public Day01() : base(01, 2023, "Trebuchet?!")
        {
            lines = Input.SplitByNewline().ToList();
        }

        protected override string SolvePartOne()
        {
            var sum = lines.Sum(line => 
                int.Parse(Regex.Match(line, @"\d").Value + Regex.Match(line, @"\d(?=[^\d]*$)").Value));

            return sum.ToString();
        }
        
        protected override string SolvePartTwo()
        {
            
            var numberWords = new Dictionary<string, string>
            {
                {"one", "1"},
                {"two", "2"},
                {"three", "3"},
                {"four", "4"},
                {"five", "5"},
                {"six", "6"},
                {"seven", "7"},
                {"eight", "8"},
                {"nine", "9"}
            };
            
            var sum = lines
                .Select(line => Regex.Matches(line, @"(?=(one|two|three|four|five|six|seven|eight|nine|\d))"))
                .Select(matches => 
                    int.Parse(TryConvertToDigits(matches.First().Groups[1].Value) + 
                              TryConvertToDigits(matches.Last().Groups[1].Value)))
                .Sum();

            return sum.ToString();

            string TryConvertToDigits(string input)
            {
                return numberWords.TryGetValue(input, out var word) ? word : input;
            }
        }
    }
}
