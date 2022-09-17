using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day12 : ASolution
    {
        readonly string[] lines;
        public Day12() : base(12, 2021, "")
        {
            lines = Input.SplitByNewline();
        }
        public class Cave
        {
            public string Name;
            public HashSet<string> Connections;
            public bool isSmall;
            public Cave(string Name)
            {
                this.Name = Name;
                this.isSmall = Name.All(x => char.IsLower(x));
                this.Connections = new();
            }
            public void AddConnection(IEnumerable<string> names)
            {
                foreach(string s in names)
                {
                    Connections.Add(s);
                }
            }
        }
        List<Cave> GetCaves()
        {
            List<Cave> list = new();
            foreach(string line in lines)
            {
                string[] nodes = line.Split("-");
                foreach(string cave in nodes)
                {
                    if(!list.Any(c => c.Name == cave))
                    {
                        list.Add(new Cave(cave));
                    }
                    list.First(c => c.Name == cave).AddConnection(nodes.Where(n => n != cave));
                }
            }
            return list;
        }
        bool Check1(Cave connection, List<Cave> path)
        {
            return connection.isSmall && connection.Name != "start" && !path.Contains(connection) || !connection.isSmall;
        }
        bool Check2(Cave connection, List<Cave> path)
        {
            List<string> smalls = path.Append(connection).Where(p => p.isSmall).Select(c => c.Name).ToList();
            return connection.isSmall && connection.Name != "start" && smalls.GroupBy(s => s).Count(c => c.Count() > 1) < 2 && !smalls.GroupBy(s => s).Any(c => c.Count() > 2) || !connection.isSmall;
        }
        int Travel(string cave, ref List<Cave> caves, List<Cave> path, bool firstPhase)
        {
            Cave caveObj = caves.Single(c => c.Name == cave);
            List<Cave> pathList = path.Append(caveObj).ToList();
            int completedPaths = 0;
            foreach (string connection in caveObj.Connections)
            {
                if (connection == "end")
                {
                    completedPaths++;
                    continue;
                }
                Cave connectionCave = caves.First(c => c.Name == connection);
                if (firstPhase && Check1(connectionCave, path) || !firstPhase && Check2(connectionCave, pathList))
                {
                    completedPaths += Travel(connection, ref caves, pathList, firstPhase);
                    continue;       
                }
            }
            return completedPaths;
        }
        protected override string SolvePartOne()
        {
            List<Cave> caves = GetCaves();
            return Travel("start", ref caves, new(), true).ToString();
        }
        protected override string SolvePartTwo()
        {
            List<Cave> caves = GetCaves();
            return Travel("start", ref caves, new(), false).ToString();
        }
    }
}