
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day03 : ASolution
{
    private enum Priority
    {
        a = 1,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z
    }
    private readonly List<string> _lines;

    public Day03() : base(03, 2022, "Rucksack Reorganization")
    {
        _lines = Input.SplitByNewline().ToList();
    }

    protected override string SolvePartOne()
    {
        return _lines.Sum(line => (int)Enum.Parse<Priority>(line[..(line.Length / 2)].First(x => line[(line.Length / 2)..].Contains(x)).ToString())).ToString();
    }

    protected override string SolvePartTwo()
    {
        var groups = _lines.GroupBy(x => _lines.IndexOf(x) / 3);
        int prioritySum = groups.Select(group => group.ToArray())
            .Select(array => (int)Enum.Parse<Priority>(array[0].Intersect(array[1]).Intersect(array[2]).First().ToString()))
            .Sum();

        return prioritySum.ToString();
    }
}
