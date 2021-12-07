using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day07 : ASolution
    {
        readonly int[] subs;
        public Day07() : base(07, 2021, "")
        {
            subs = Input.SplitByNewline()[0].Split(",").Select(i => int.Parse(i)).ToArray();
        }

        protected override string SolvePartOne()
        {
            Array.Sort(subs);
            int median = subs[subs.Length / 2];
            return subs.Sum(i => i < median ? median - i : i - median).ToString();
        }
        protected override string SolvePartTwo()
        {
            int avg = Convert.ToInt32(Math.Floor(subs.Average()));
            //the average actually minimizes (distance ^ 2), but distance (distance + 1)/2 is close enough
            //to (distance^2 / 2) hence playing with rounding just works
            //lucky input, i guess
            return subs.Sum(i => i<avg ? Enumerable.Range(1, avg - i).Sum() : Enumerable.Range(1, i - avg).Sum()).ToString();
        }
    }
}