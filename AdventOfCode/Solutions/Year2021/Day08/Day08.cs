using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day08 : ASolution
{
    private readonly string[] _lines;
    public Day08() : base(08, 2021, "")
    {
        _lines = Input.SplitByNewline();
    }

    protected override string SolvePartOne()
    {
        string[] outputValues = _lines.Select(i => i.Split(" | ")[1]).ToArray();
        return outputValues.Select(x => x.Split(" ").Count(i => new int[] {2, 3, 4, 7}.Contains(i.Length))).Sum().ToString();
    }
    protected override string SolvePartTwo()
    {
        int total = 0;
        int GetNumber(string[] outValues, char[][] combinations)
        {
            int number = 0;
            number += 1000 * Array.IndexOf(combinations, combinations.Where(c => c.Length == outValues[0].Length && c.Intersect(outValues[0].ToCharArray()).Count() == c.Length).First());
            number += 100 * Array.IndexOf(combinations, combinations.Where(c => c.Length == outValues[1].Length && c.Intersect(outValues[1].ToCharArray()).Count() == c.Length).First());
            number += 10 * Array.IndexOf(combinations, combinations.Where(c => c.Length == outValues[2].Length && c.Intersect(outValues[2].ToCharArray()).Count() == c.Length).First());
            number += Array.IndexOf(combinations, combinations.Where(c => c.Length == outValues[3].Length && c.Intersect(outValues[3].ToCharArray()).Count() == c.Length).First());
            Console.WriteLine(number);
            return number;
        }
        foreach(string line in _lines)
        {
            string[] digits = line.Split(" | ")[0].Split(" ");
            char[][] combinations = new char[10][];
            combinations[1] = digits.Where(d => d.Length == 2).First().ToCharArray();
            combinations[7] = digits.Where(d => d.Length == 3).First().ToCharArray();
            combinations[4] = digits.Where(d => d.Length == 4).First().ToCharArray();
            combinations[8] = digits.Where(d => d.Length == 7).First().ToCharArray();
            combinations[6] = digits.Where(d => d.Length == 6 && !combinations[1].All(i => d.Contains(i))).First().ToCharArray();
            combinations[9] = digits.Where(d => d.Length == 6 && combinations[4].All(i => d.Contains(i))).First().ToCharArray();
            combinations[0] = digits.Where(d => d.Length == 6 && !combinations[6].All(i => d.Contains(i)) && !combinations[9].All(i => d.Contains(i))).First().ToCharArray();
            combinations[3] = digits.Where(d => d.Length == 5 && combinations[1].All(i => d.Contains(i))).First().ToCharArray();
            combinations[5] = digits.Where(d => d.Length == 5 && d.All(i => combinations[6].Contains(i))).First().ToCharArray();
            combinations[2] = digits.Where(d => d.Length == 5 && d.ToCharArray().Intersect(combinations[5]).Count() == 3).First().ToCharArray();
            total += GetNumber(line.Split(" | ")[1].Split(" "), combinations);
        }
        return total.ToString();
    }
}