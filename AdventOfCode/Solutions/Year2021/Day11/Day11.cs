using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day11 : ASolution
    {
        readonly string[] lines;
        public Day11() : base(11, 2021, "")
        {
            lines = Input.SplitByNewline();
        }
        public record Point(int X, int Y);
        public class Octopus
        {
            public Point point;
            public int Power;
            public int Flashes;
            public bool Flashing;
            public Octopus(int x, int y, int power)
            {
                point = new(x, y);
                Power = power;
                Flashes = 0;
                Flashing = false;
            }
        }
        List<Octopus> GetOctopi()
        {
            List<Octopus> octopi = new();
            for (int i = 0; i < lines.Length * lines[0].Length; i++)
            {
                octopi.Add(new Octopus(i / lines.Length, i % lines[0].Length, (int)char.GetNumericValue(lines[i / lines.Length][i % lines[0].Length])));
            }
            return octopi;
        }
        static void Advance(Octopus o)
        {
            if(o != null && !o.Flashing)
                o.Power++;
        }
        void Flash(Octopus o, List<Octopus> octopi)
        {
            o.Power = 0;
            o.Flashes++;
            o.Flashing = true;
            Advance(octopi.Where(i => i.point.X == o.point.X - 1 && i.point.Y == o.point.Y - 1).FirstOrDefault());
            Advance(octopi.Where(i => i.point.X == o.point.X && i.point.Y == o.point.Y - 1).FirstOrDefault());
            Advance(octopi.Where(i => i.point.X == o.point.X + 1 && i.point.Y == o.point.Y - 1).FirstOrDefault());
            Advance(octopi.Where(i => i.point.X == o.point.X - 1 && i.point.Y == o.point.Y).FirstOrDefault());
            Advance(octopi.Where(i => i.point.X == o.point.X + 1 && i.point.Y == o.point.Y).FirstOrDefault());
            Advance(octopi.Where(i => i.point.X == o.point.X - 1 && i.point.Y == o.point.Y + 1).FirstOrDefault());
            Advance(octopi.Where(i => i.point.X == o.point.X && i.point.Y == o.point.Y + 1).FirstOrDefault());
            Advance(octopi.Where(i => i.point.X == o.point.X + 1 && i.point.Y == o.point.Y + 1).FirstOrDefault());
        }
        protected override string SolvePartOne()
        {
            List<Octopus> octopi = GetOctopi();
            for(int i = 0; i< 100; i++)
            {
                octopi.Select(o => { o.Flashing = false; return o; }).ToList();
                foreach (Octopus o in octopi)
                {
                    Advance(o);
                }
                while (octopi.Any(o => o.Power > 9))
                {
                    foreach (Octopus o in octopi.Where(o => o.Power > 9))
                    {
                        Flash(o, octopi);
                    }
                }
            }
            return octopi.Sum(o => o.Flashes).ToString();
        }
        protected override string SolvePartTwo()
        {
            List<Octopus> octopi = GetOctopi();
            int step = 0;
            while(!octopi.All(o => o.Flashing))
            {
                step++;
                octopi.Select(o => { o.Flashing = false; return o; }).ToList();
                foreach (Octopus o in octopi)
                {
                    Advance(o);
                }
                while (octopi.Any(o => o.Power > 9))
                {
                    foreach (Octopus o in octopi.Where(o => o.Power > 9))
                    {
                        Flash(o, octopi);
                    }
                }
            }
            return step.ToString();
        }
    }
}