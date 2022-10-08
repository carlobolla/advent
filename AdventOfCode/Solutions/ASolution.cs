using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdventOfCode.Infrastructure.Exceptions;
using AdventOfCode.Infrastructure.Helpers;
using AdventOfCode.Infrastructure.Models;

namespace AdventOfCode.Solutions;

internal abstract class ASolution
{
    private readonly Lazy<string> _input;
    private readonly Lazy<string> _debugInput;
    private readonly Lazy<SolutionResult> _part1;
    private readonly Lazy<SolutionResult> _part2;

    public int Day { get; }
    public int Year { get; }
    public string Title { get; }
    public string Input => Debug ? DebugInput : _input.Value ?? null;
    public SolutionResult Part1 => _part1.Value;
    public SolutionResult Part2 => _part2.Value;

    public string DebugInput => _debugInput.Value ?? null;
    public bool Debug { get; set; }

    private protected ASolution(int day, int year, string title, bool useDebugInput = false)
    {
        Day = day;
        Year = year;
        Title = title;
        Debug = useDebugInput;
        _input = new Lazy<string>(() => InputHelper.LoadInput(Day, Year));
        _debugInput = new Lazy<string>(() => InputHelper.LoadDebugInput(Day, Year));
        _part1 = new Lazy<SolutionResult>(() => SolveSafely(SolvePartOne));
        _part2 = new Lazy<SolutionResult>(() => SolveSafely(SolvePartTwo));
    }

    public IEnumerable<SolutionResult> Solve(int part = 0)
    {
        if (part != 2 && (part == 1 || !string.IsNullOrEmpty(Part1.Answer)))
        {
            yield return Part1;
        }

        if (part != 1 && (part == 2 || !string.IsNullOrEmpty(Part2.Answer)))
        {
            yield return Part2;
        }
    }

    private SolutionResult SolveSafely(Func<string> solverFunction)
    {
        if (Debug)
        {
            if (string.IsNullOrEmpty(DebugInput))
            {
                throw new InputException("DebugInput is null or empty");
            }
        }
        else if (string.IsNullOrEmpty(Input))
        {
            throw new InputException("Input is null or empty");
        }

        try
        {
            var then = DateTime.Now;
            var result = solverFunction();
            var now = DateTime.Now;
            return string.IsNullOrEmpty(result) ? SolutionResult.Empty : new SolutionResult { Answer = result, Time = now - then };
        }
        catch (Exception)
        {
            if (Debugger.IsAttached)
            {
                throw;
            }
            else
            {
                throw;
            }
        }
    }

    protected abstract string SolvePartOne();
    protected abstract string SolvePartTwo();
}