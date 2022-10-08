using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day02 : ASolution
{
    private readonly string[] _lines;
    public Day02() : base(02, 2021, "Dive!")
    {
        _lines = Input.SplitByNewline();
    }

    protected override string SolvePartOne()
    {
        int position = 0, depth = 0;
        foreach (string line in _lines)
        {
            string[] instruction = line.Split(" ");
            switch (instruction[0])
            {
                case "forward":
                    position += Convert.ToInt32(instruction[1]);
                    break;
                case "up":
                    depth -= Convert.ToInt32(instruction[1]);
                    break;
                case "down":
                    depth += Convert.ToInt32(instruction[1]);
                    break;
            }
        }
        return (position * depth).ToString();
    }

    protected override string SolvePartTwo()
    {
        int position = 0, depth = 0, aim = 0;
        foreach (string line in _lines)
        {
            string[] instruction = line.Split(" ");
            switch (instruction[0])
            {
                case "forward":
                    position += Convert.ToInt32(instruction[1]);
                    depth += aim*Convert.ToInt32(instruction[1]);
                    break;
                case "up":
                    aim -= Convert.ToInt32(instruction[1]);
                    break;
                case "down":
                    aim += Convert.ToInt32(instruction[1]);
                    break;
            }
        }
        return (position * depth).ToString();
    }
}