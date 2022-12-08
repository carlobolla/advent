using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day07 : ASolution
{
    private readonly string[] _lines;
    public Day07() : base(07, 2022, "No Space Left On Device")
    {
        _lines = Input.SplitByNewline();
    }

    private static Dictionary<string, int> GetFolders(IEnumerable<string> lines)
    {
        Stack<string> currentPath = new();
        // Yes, i did it with a dictionary. Sue me.
        Dictionary<string, int> folders = new();
        foreach (string line in lines)
        {
            if (line == "$ ls") continue;
            
            if (line.StartsWith("$ cd"))
            {
                if(line.Contains("..")) 
                    currentPath.Pop();
                else
                {
                    currentPath.Push(line[5..]);
                    folders.TryAdd(string.Join("/", currentPath), 0);
                }
                continue;
            }
                
            if (line.StartsWith("dir "))
            {
                folders.TryAdd(line[4..] + string.Join("/", currentPath), 0);
                continue;
            }
            
            for(int i = 0; i < currentPath.Count; i++)
            {
                folders[string.Join("/", currentPath.ToArray()[i..])] += int.Parse(line.Split(' ')[0]);
            }
        }

        return folders;
    }

    protected override string SolvePartOne()
    {
        var folders = GetFolders(_lines);
        return folders.Where(f => f.Value <= 100000).Sum(pair => pair.Value).ToString();
    }

    protected override string SolvePartTwo()
    {
        var folders = GetFolders(_lines);
        return folders.Where(f => f.Value > 30000000 - (70000000 - folders["/"])).Min(f => f.Value).ToString();
    }
}
