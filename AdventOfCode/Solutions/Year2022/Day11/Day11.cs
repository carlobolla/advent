using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2022;

internal class Day11 : ASolution
{
    private static Func<long, long> GetOperation(string line)
    {
        var operationMatch = Regex.Match(line, @"  Operation: new = old ([+*]) (old|\d*)");
        int.TryParse(operationMatch.Groups[2].Value, out int operand);
        Func<long, long> operation = operationMatch.Groups[1].Value switch
        {
            "+" => a => a + (operand == 0 ? a : operand),
            "*" => a => a * (operand == 0 ? a : operand),
            _ => throw new Exception("Unknown operation")
        };
        return operation;
    }
    
    private class Monkey
    {
        public readonly Queue<long> Items;
        public readonly Func<long, long> Operation;
        public readonly int Mod;
        public readonly int IfTrue;
        public readonly int IfFalse;
        public int Inspections;

        public Monkey(IEnumerable<long> items, Func<long, long> operation, int mod, int ifTrue, int ifFalse)
        {
            Items = new Queue<long>(items);
            Operation = operation;
            Mod = mod;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
            Inspections = 0;
        }
    }

    Monkey[] GetMonkeys()
    {
        return Input.Split("\n\n").Select(split =>
        {
            return new Monkey(
                Regex.Match(split, @"  Starting items:( (\d+),?)+").Groups[2].Captures.Select(x => long.Parse(x.Value)),
                GetOperation(split),
                int.Parse(Regex.Match(split, @"  Test: divisible by (\d+)").Groups[1].Value),
                int.Parse(Regex.Match(split, @"    If true: throw to monkey (\d+)").Groups[1].Value),
                int.Parse(Regex.Match(split, @"    If false: throw to monkey (\d+)").Groups[1].Value)
            );
        }).ToArray();
    }
    
    public Day11() : base(11, 2022, "Monkey in the Middle")
    {
    }

    private void Run(int rounds, Monkey[] monkeys, Func<long, long> decreaseWorry)
    {
        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                while(monkey.Items.Any())
                {
                    monkey.Inspections++;
                    long item = monkey.Items.Dequeue();
                    item = monkey.Operation(item);
                    item = decreaseWorry(item);
                    monkeys[item % monkey.Mod == 0 ? monkey.IfTrue : monkey.IfFalse].Items.Enqueue(item);
                }
            }
        }
    }

    protected override string SolvePartOne()
    {
        Monkey[] monkeys = GetMonkeys();
        Run(20, monkeys, w => w/3);
        return monkeys.OrderByDescending(m => m.Inspections).Take(2)
            .Aggregate(1L, (i, monkey) => i * monkey.Inspections).ToString();
    }

    protected override string SolvePartTwo()
    {
        Monkey[] monkeys = GetMonkeys();
        var mod = monkeys.Aggregate(1, (mod, monkey) => mod * monkey.Mod);
        Run(10000, monkeys, w => w % mod);
        return monkeys.OrderByDescending(m => m.Inspections).Take(2)
            .Aggregate(1L, (i, monkey) => i * monkey.Inspections).ToString();
    }
}
