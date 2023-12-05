using System;
using System.Linq;
using AdventOfCode.Infrastructure;

namespace AdventOfCode;

internal static class Program
{
    private static void Main(string[] args)
    {
        var solutions = args.Length == 0 ? new SolutionCollector() 
            : new SolutionCollector(int.Parse(args[0]), args.Length > 1 
            ? args[1].Split(',').Select(int.Parse).ToArray() : Array.Empty<int>());
        foreach (var solution in solutions)
        {
            Console.WriteLine();
            Console.WriteLine(FormatHelper.FunctionFormat(solution));
        }
    }
}