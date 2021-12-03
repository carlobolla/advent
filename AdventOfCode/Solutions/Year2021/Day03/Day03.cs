using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day03 : ASolution
    {
        readonly string[] lines;
        public Day03() : base(03, 2021, "")
        {
            lines = Input.SplitByNewline();
        }

        protected override string SolvePartOne()
        {
            char[] most_common = new char[12];
            char[] least_common = new char[12];
            foreach(int i in Enumerable.Range(0,12))
            {
                most_common[i] = lines.Count(bit => bit[i] == '0') > lines.Length / 2 ? '0' : '1';
                least_common[i] = lines.Count(bit => bit[i] == '0') < lines.Length / 2 ? '0' : '1';
            }
            uint gamma = Convert.ToUInt32(new string(most_common), 2);
            uint epsilon = Convert.ToUInt32(new string(least_common), 2); ;

            return (gamma*epsilon).ToString();
        }

        protected override string SolvePartTwo()
        {
            int oxy_gen_rating(string[] lines)
            {
                foreach (int i in Enumerable.Range(0, 12))
                {
                    lines = lines.Where(x => x[i] == (lines.Count(bit => bit[i] == '0') > lines.Length / 2 ? '0' : '1')).ToArray();
                    if (lines.Length == 1)
                        return Convert.ToInt32(lines[0], 2);
                }
                return 0;
            }
            int co2_scrub_rating(string[] lines)
            {
                foreach (int i in Enumerable.Range(0, 12))
                {
                    lines = lines.Where(x => x[i] == (lines.Count(bit => bit[i] == '0') <= lines.Length / 2 ? '0' : '1')).ToArray();
                    if (lines.Length == 1)
                        return Convert.ToInt32(lines[0], 2);
                }
                return 0;
            }

            return (oxy_gen_rating(lines) * co2_scrub_rating(lines)).ToString();
        }
    }
}