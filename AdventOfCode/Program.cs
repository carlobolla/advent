using System;
using AdventOfCode.Infrastructure;
using AdventOfCode.Infrastructure.Helpers;
using AdventOfCode.Solutions;

namespace AdventOfCode;

internal class Program
{
    private static readonly SolutionCollector _solutions = new SolutionCollector();

    private static void Main(string[] args)
    {
        foreach (ASolution solution in _solutions)
        {
            Console.WriteLine();
            Console.WriteLine(FormatHelper.FunctionFormat(solution));
        }
    }
}