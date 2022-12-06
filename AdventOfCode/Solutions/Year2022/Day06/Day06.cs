using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day06 : ASolution
{
    private readonly string _input;
    public Day06() : base(06, 2022, "Tuning Trouble")
    {
        _input = Input;
    }

    private static int? FindPacket(string s, int length)
    {
        for (int i = 0; i < s.Length - length; i++)
        {
            var packet = s[i..(i + length)];
            if (packet.All(c => packet.Count(c2 => c2 == c) == 1))
            {
                return i + length;
            }
        }

        return null;
    }

    protected override string SolvePartOne()
    {
        return FindPacket(_input, 4).ToString() ?? "Not found";
    }

    protected override string SolvePartTwo()
    {
        return FindPacket(_input, 14).ToString() ?? "Not found";
    }
}
