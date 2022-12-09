using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day18 : ASolution
{
    // I admit cheating on this one. I'm studying why this works. Thanks https://github.com/encse.
    private enum TokenKind {
        Open,
        Close,
        Digit
    }
    private record Token(TokenKind Kind, int Value = 0);

    private class Number : List<Token> {
        public static Number Digit(int value) =>
            new (){
                new Token(TokenKind.Digit, value)
            };

        public static Number Pair(Number a, Number b) {
            var number = new Number { new (TokenKind.Open) };
            number.AddRange(a);
            number.AddRange(b);
            number.Add(new Token(TokenKind.Close));
            return number;
        }
    };
    
    private static long Magnitude(Number number) {
        var iToken = 0;

        long ComputeRecursive() {
            var token = number[iToken++];
            if (token.Kind == TokenKind.Digit) {
                return token.Value;
            } else {
                var left = ComputeRecursive();
                var right = ComputeRecursive();
                iToken++;
                return 3 * left + 2 * right;
            }
        }

        return ComputeRecursive();
    }
    
    private static Number Sum(Number numberA, Number numberB) => Reduce(Number.Pair(numberA, numberB));

    private static Number Reduce(Number number) {
        while (Explode(number) || Split(number)) {
        }
        return number;
    }
    
    private static bool Split(Number number) {
        for (var i = 0; i < number.Count; i++)
        {
            if (number[i].Value < 10) continue;
            var v = number[i].Value;
            number.RemoveRange(i, 1);
            number.InsertRange(i, Number.Pair(Number.Digit(v / 2), Number.Digit((v + 1) / 2)));
            return true;
        }
        return false;
    }

    private static bool Explode(Number number) {
        var depth = 0;
        for (var i = 0; i < number.Count; i++)
        {
            switch (number[i].Kind)
            {
                case TokenKind.Open:
                    depth++;
                    if (depth == 5) {
                        for (var j = i - 1; j >= 0; j--)
                        {
                            if (number[j].Kind != TokenKind.Digit) continue;
                            number[j] = number[j] with { Value = number[j].Value + number[i + 1].Value };
                            break;
                        }
                        for (var j = i + 3; j < number.Count; j++)
                        {
                            if (number[j].Kind != TokenKind.Digit) continue;
                            number[j] = number[j] with { Value = number[j].Value + number[i + 2].Value };
                            break;
                        }
                        number.RemoveRange(i, 4);
                        number.Insert(i, new Token(TokenKind.Digit, 0));
                        return true;
                    }
                    break;
                case TokenKind.Close:
                    depth--;
                    break;
            }
        }
        return false;
    }

    private static Number ParseNumber(string st) {
        var res = new Number();
        var n = "";
        foreach (var ch in st) {
            if (ch is >= '0' and <= '9') {
                n += ch;
            } else {
                if (n != "") {
                    res.Add(new Token(TokenKind.Digit, int.Parse(n)));
                    n = "";
                }
                if (ch == '[') {
                    res.Add(new Token(TokenKind.Open));
                } else if (ch == ']') {
                    res.Add(new Token(TokenKind.Close));
                }
            }
        }

        if (n == "") return res;
        res.Add(new Token(TokenKind.Digit, int.Parse(n)));
        return res;
    }
    
    private readonly string[] _lines;

    public Day18() : base(18, 2021, "Snailfish")
    {
        _lines = Input.SplitByNewline();
    }

    protected override string SolvePartOne()
    {
        return _lines.Select(ParseNumber).Aggregate(
            new Number(),
            (acc, number) => !acc.Any() ? number : Sum(acc, number),
            Magnitude
        ).ToString();
    }

    protected override string SolvePartTwo()
    {
        return Enumerable.Range(0, _lines.Length)
            .SelectMany(i => Enumerable.Range(0, _lines.Length), (i, j) => new { i, j })
            .Select(t => Magnitude(Sum(ParseNumber(_lines[t.i]), ParseNumber(_lines[t.j])))).Max().ToString();
    }
}