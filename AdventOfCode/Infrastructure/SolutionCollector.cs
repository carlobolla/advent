using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Infrastructure.Models;
using AdventOfCode.Solutions;

namespace AdventOfCode.Infrastructure;

internal class SolutionCollector : IEnumerable<ASolution>
{
    private static readonly Config Config = Config.Get("config.json");

    private readonly IEnumerable<ASolution> _solutions;

    public SolutionCollector() => _solutions = LoadSolutions(Config.Year, Config.Days).ToArray();
    public SolutionCollector(int year, int[] days) => _solutions = LoadSolutions(year, days).ToArray();

    public ASolution? GetSolution(int day)
    {
        try
        {
            return _solutions.Single(s => s.Day == day);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public IEnumerator<ASolution> GetEnumerator() => _solutions.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IEnumerable<ASolution> LoadSolutions(int year, int[] days)
    {
        if (days.Sum() == 0)
        {
            days = Enumerable.Range(1, 25).ToArray();
        }

        foreach (int day in days)
        {
            var solution = Type.GetType($"AdventOfCode.Solutions.Year{year}.Day{day.ToString("D2")}");
            if (solution != null)
            {
                yield return (ASolution)Activator.CreateInstance(solution)!;
            }
        }
    }
}