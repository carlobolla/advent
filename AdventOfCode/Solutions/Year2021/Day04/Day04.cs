using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day04 : ASolution
    {
        //this one is truly horrible.
        //try to refactor using OOP, please :/
        readonly string[] lines;
        readonly int[] extracts;
        readonly List<int[][]> boards;
        public Day04() : base(04, 2021, "")
        {
            lines = Input.SplitByNewline().Where(line => line != "").ToArray();
            extracts = lines[0].Split(',').Select(x=>Convert.ToInt32(x)).ToArray();
            boards = new();
            foreach (string[] board in lines[1..].Chunk(5))
            {
                int[][] boardToAdd = new int[5][];
                foreach (var row in board.Select((value, i) => (value, i)))
                {
                    boardToAdd[row.i] = row.value.Trim().Replace("  ", " ").Split(" ").Select(x => Convert.ToInt32(x)).ToArray();
                }
                boards.Add(boardToAdd);
            }
        }

        protected override string SolvePartOne()
        {
            bool checkColumn(int[][] board, int column, int[] extracts)
            {
                foreach (int rowIndex in Enumerable.Range(0, board.GetLength(0)))
                {
                    if (!extracts.Contains(board[rowIndex][column]))
                        return false;
                }
                return true;
            }
            bool checkRow(int[] row, int[] extracts)
            {
                if (!row.All(i => extracts.Contains(i)))
                    return false;
                return true;
            }
            for (int e = 1; e<extracts.Length; e++)
            {
                foreach (int[][] board in boards)
                {
                    foreach (int colIndex in Enumerable.Range(0, board.GetLength(0)))
                    {
                        if (checkColumn(board, colIndex, extracts[..e]) || checkRow(board[colIndex], extracts[..e]))
                        {
                            return (board.SelectMany(x => x).ToList().Where(i => !extracts[..e].Contains(i)).Sum() * extracts[e-1]).ToString();
                        }
                    }
                }
            }
            return null;
        }

        protected override string SolvePartTwo()
        {
            int lastWinner = 0;
            int extractAtWin = 0;
            int e;
            List<int> winningBoards = new List<int>();
            bool checkColumn(int[][] board, int column, int[] extracts)
            {
                foreach (int rowIndex in Enumerable.Range(0, board.GetLength(0)))
                {
                    if (!extracts.Contains(board[rowIndex][column]))
                        return false;
                }
                return true;
            }
            bool checkRow(int[] row, int[] extracts)
            {
                if (!row.All(i => extracts.Contains(i)))
                    return false;
                return true;
            }
            for (e = 1; e < extracts.Length; e++)
            {
                for (int x = 0; x < boards.Count; x++)
                {
                    if (!winningBoards.Contains(boards.IndexOf(boards[x])))
                    {
                        foreach (int colIndex in Enumerable.Range(0, boards[x].GetLength(0)))
                        {
                            if (checkColumn(boards[x], colIndex, extracts[..e]) || checkRow(boards[x][colIndex], extracts[..e]))
                            {
                                lastWinner = boards.IndexOf(boards[x]);
                                extractAtWin = extracts[e-1];
                                winningBoards.Add(x);
                            }
                        }
                    }  
                }
            }
            return (boards[lastWinner].SelectMany(x => x).ToList().Where(i => !extracts[..(Array.IndexOf(extracts, extractAtWin)+1)].Contains(i)).Sum() * extractAtWin).ToString();
        }
    }
}