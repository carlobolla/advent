using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day10 : ASolution
    {
        readonly string[] lines;
        public Day10() : base(10, 2021, "")
        {
            lines = Input.SplitByNewline();
        }
        Stack getOpenParentheses(string line)
        {
            Stack s = new();
            foreach (char c in line)
            {
                if (new char[] { '<', '[', '(', '{' }.Contains(c))
                    s.Push(c);
                else
                    s.Pop();
            }
            return s;
        }
        int getCorruptionScore(string line)
        {
            Stack s = new();
            foreach(char c in line)
            {
                if (new char[] {'<', '[', '(', '{'}.Contains(c))
                    s.Push(c);
                else
                    switch (c)
                    {
                        case '>':
                            if ((char)s.Pop() != '<')
                                return 25137;
                            break;
                        case ']':
                            if ((char)s.Pop() != '[')
                                return 57;
                            break;
                        case ')':
                            if ((char)s.Pop() != '(')
                                return 3;
                            break;
                        case '}':
                            if ((char)s.Pop() != '{')
                                return 1197;
                            break;
                        default:
                            Console.WriteLine("OOOPS!");
                            Environment.Exit(0);
                            break;
                    }
            }
            return 0;
        }
        protected override string SolvePartOne()
        {
            return lines.Select(l => getCorruptionScore(l)).Sum().ToString();
        }
        protected override string SolvePartTwo()
        {
            List<long> scores = new();
            string[] incompleteLines = lines.Where(l => getCorruptionScore(l) == 0).ToArray();
            foreach(string line in incompleteLines)
            {
                long score = 0;
                foreach(char parenthesis in getOpenParentheses(line))
                {
                    score *= 5;
                    switch (parenthesis)
                    {
                        case '(':
                            score++;
                            break;
                        case '[':
                            score += 2;
                            break;
                        case '{':
                            score += 3;
                            break;
                        case '<':
                            score += 4;
                            break;
                        default:
                            Console.WriteLine("OOPS!");
                            break;
                    }
                }
                scores.Add(score);
            }
            scores.Sort();
            return scores[scores.Count/2].ToString();
        }
    }
}