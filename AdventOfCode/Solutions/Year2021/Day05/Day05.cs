using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day05 : ASolution
    {
        readonly string[] lines;
        readonly List<Pipe> pipes = new();
        public Day05() : base(05, 2021, "")
        {
            lines = Input.SplitByNewline();
            foreach(string line in lines)
            {
                string[] splits = line.Split(" -> ");
                int[] coordinates1 = splits[0].Split(",").Select(i => int.Parse(i)).ToArray();
                int[] coordinates2 = splits[1].Split(",").Select(i => int.Parse(i)).ToArray();
                Point point1 = new(coordinates1[0], coordinates1[1]);
                Point point2 = new(coordinates2[0], coordinates2[1]);
                pipes.Add(new(point1, point2));
            }
        }

        protected override string SolvePartOne()
        {
            return pipes.Where(i => i.Straight).SelectMany(i=>i.Path).GroupBy(i=>i).Where(i=>i.Count() >= 2).Count().ToString();   
        }

        protected override string SolvePartTwo()
        {
            return pipes.SelectMany(i => i.Path).GroupBy(i => i).Where(i => i.Count() >= 2).Count().ToString();
        }
    }
}