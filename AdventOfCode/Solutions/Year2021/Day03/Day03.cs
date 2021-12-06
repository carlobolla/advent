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
            foreach(int i in Enumerable.Range(0,12))
            {
                most_common[i] = lines.Count(bit => bit[i] == '0') > lines.Length / 2 ? '0' : '1';
            }
            uint gamma = Convert.ToUInt16(new string(most_common), 2);
            uint epsilon = gamma ^ 0b0000111111111111; //flip only 12 bits by XOR-ing with a mask
            return (gamma*epsilon).ToString();
        }

        protected override string SolvePartTwo()
        {
            string[] oxyValues = lines.Where(x => x[0] == (lines.Count(bit => bit[0] == '0') > lines.Length / 2 ? '0' : '1')).ToArray();
            string[] co2Values = lines.Where(x => !oxyValues.Contains(x)).ToArray();
            foreach (int i in Enumerable.Range(0, 10))
            {
                if (oxyValues.Length != 1)
                {
                    oxyValues = oxyValues.Where(x => x[i + 1] == (oxyValues.Count(bit => bit[i+1] == '0') > oxyValues.Length / 2 ? '0' : '1')).ToArray();
                }
                if (co2Values.Length != 1)
                {
                    co2Values = co2Values.Where(x => x[i+1] == (co2Values.Count(bit => bit[i+1] == '0') <= co2Values.Length / 2 ? '0' : '1')).ToArray();
                }
            }
            return (Convert.ToInt32(oxyValues[0], 2) * Convert.ToInt32(co2Values[0], 2)).ToString();
        }
    }
}