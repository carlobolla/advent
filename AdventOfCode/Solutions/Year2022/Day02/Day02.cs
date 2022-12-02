using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day02 : ASolution
{
    private static int? StringToSign(string s)
    {
        switch (s)
        {
            case "A":
            case "X":
                return 0;
            case "B":
            case "Y":
                return 1;
            case "C":
            case "Z":
                return 2;
        }
        return null;
    }

    private static int? ScorePhase1(int? a, int? b)
    {
        switch (b - a)
        {
            case -2:
            case 1:
                return (int)b! + 7; // a wins, +6
            case -1:
            case 2:
                return (int)b! + 1; // b wins, +0
            case 0:
                return (int)b! + 4; // draw, +3
        }

        return null;
    }

    private static int? ScorePhase2(int? a, string outcome)
    {
        return outcome switch
        {
            "X" => a == 0 ? 3 : a,
            "Y" => a + 4,
            "Z" => a == 2 ? 7 : a + 8,
            _ => null
        };
    }

    private readonly List<string[]> _lines;
    public Day02() : base(02, 2022, "Rock Paper Scissors")
    {
        _lines = Input.SplitByNewline().ToList().Select(x => x.Split(" ")).ToList();
    }

    protected override string SolvePartOne()
    {
        return _lines.Sum(x => ScorePhase1(StringToSign(x[0]), StringToSign(x[1]))).ToString()!;
    }

    protected override string SolvePartTwo()
    {
        return _lines.Sum(x => ScorePhase2(StringToSign(x[0])!, x[1])).ToString()!;
    }
}
