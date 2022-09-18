using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Year2021
{
    class Day13 : ASolution
    {
        readonly List<Tuple<int, int>> dots;
        Grid grid;
        List<string> folds;
        public Day13() : base(13, 2021, "")
        {
            dots = Input.SplitByNewline().Where(l => !l.StartsWith("fold") && l != "").Select(x => Tuple.Create<int, int>(int.Parse(x.Split(',')[0]), int.Parse(x.Split(',')[1]))).ToList();
            grid = new Grid(dots);
            folds = Input.SplitByNewline().Where(l => l.StartsWith("fold")).Select(s => s.Replace("fold along ", "")).ToList();
        }

        public class Grid{
            public bool[,] grid;
            public Grid(List<Tuple<int, int>> dots)
            {
                int h = dots.Max(d => d.Item2) + 1;
                int w = dots.Max(d => d.Item1) + 1;
                grid = new bool[h % 2 == 0 ? h+1 : h, w % 2 == 0 ? w+1 : w];
                dots.ForEach(dot => grid[dot.Item2, dot.Item1] = true);
            }

            //fold along x
            public void FoldOnCol(int colToFold)
            {
                int newGridLength = Math.Max(grid.GetLength(1) - colToFold - 1, colToFold);
                bool[,] tempGrid = new bool[grid.GetLength(0), newGridLength % 2 == 0 ? newGridLength + 1 : newGridLength];
                for (int row = 0; row < tempGrid.GetLength(0); row++)
                {
                    for(int col = 0; col < tempGrid.GetLength(1); col++)
                    {
                        tempGrid[row, col] = grid[row, col] || grid[row, grid.GetLength(1) - 1 - col];
                    }
                }
                grid = tempGrid;
            }

            //fold along y
            public void FoldOnRow(int rowToFold)
            {
                int newGridLength = Math.Max(rowToFold, grid.GetLength(0) - rowToFold - 1);
                bool[,] tempGrid = new bool[newGridLength % 2 == 0 ? newGridLength + 1 : newGridLength, grid.GetLength(1)];
                for (int row = 0; row < tempGrid.GetLength(0); row++)
                {
                    for (int col = 0; col < tempGrid.GetLength(1); col++)
                    {
                        tempGrid[row, col] = grid[row, col] || grid[grid.GetLength(0) - 1 - row, col];
                    }
                }
                grid = tempGrid;
            }

            override public string ToString()
            {
                string gridString = "\n";
                for(int row = 0; row < grid.GetLength(0); row++)
                {
                    for(int col = 0; col < grid.GetLength(1); col++)
                    {
                        gridString += grid[row, col] ? "█" : " ";
                    }
                    gridString += "\n";
                }
                return gridString;
            }
        }

        protected override string SolvePartOne()
        {
            if (folds[0].StartsWith("x"))
                grid.FoldOnCol(int.Parse(folds[0][2..]));
            else
                grid.FoldOnRow(int.Parse(folds[0][2..]));
            return grid.grid.Flatten().Count(x => x).ToString();
        }

        protected override string SolvePartTwo()
        {
            folds.GetRange(1, folds.Count - 1).ForEach(fold =>
            {
                if (fold.StartsWith("x"))
                    grid.FoldOnCol(int.Parse(fold[2..]));
                else
                    grid.FoldOnRow(int.Parse(fold[2..]));
            });
            return grid.ToString();
        }
    }
}
