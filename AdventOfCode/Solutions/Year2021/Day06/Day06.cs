using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day06 : ASolution
    {
        readonly string[] lines;
        long[] feeshByTimer = new long[9];
        void advanceDays(long[] feeshByDays, int days)
        {
            for (int d = 0; d < days; d++)
            {
                feeshByDays[(d + 7) % 9] += feeshByDays[d % 9];
            }
        }
        public Day06() : base(06, 2021, "")
        {
            lines = Input.SplitByNewline();
        }

        protected override string SolvePartOne()
        {
            foreach(int i in lines[0].Split(",").Select(i => int.Parse(i)))
            {
                feeshByTimer[i]++;
            }
            advanceDays(feeshByTimer, 80);
            return feeshByTimer.Sum().ToString();
        }

        protected override string SolvePartTwo()
        {
            feeshByTimer = new long[9];
            foreach (int i in lines[0].Split(",").Select(i => int.Parse(i)))
            {
                feeshByTimer[i]++;
            }
            advanceDays(feeshByTimer, 256);
            return feeshByTimer.Sum().ToString();
        }
    }
}