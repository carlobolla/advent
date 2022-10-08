using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day04 : ASolution
{
    //this one is truly horrible.
    //try to refactor using OOP, please :/
    private readonly string[] _lines;
    private readonly int[] _extracts;
    private readonly List<int[][]> _boards;
    public Day04() : base(04, 2021, "")
    {
        _lines = Input.SplitByNewline().Where(line => line != "").ToArray();
        _extracts = _lines[0].Split(',').Select(x=>Convert.ToInt32(x)).ToArray();
        _boards = new List<int[][]>();
        foreach (string[] board in _lines[1..].Chunk(5))
        {
            int[][] boardToAdd = new int[5][];
            foreach (var row in board.Select((value, i) => (value, i)))
            {
                boardToAdd[row.i] = row.value.Trim().Replace("  ", " ").Split(" ").Select(x => Convert.ToInt32(x)).ToArray();
            }
            _boards.Add(boardToAdd);
        }
    }

    protected override string SolvePartOne()
    {
        bool CheckColumn(int[][] board, int column, int[] extracts)
        {
            foreach (int rowIndex in Enumerable.Range(0, board.GetLength(0)))
            {
                if (!extracts.Contains(board[rowIndex][column]))
                    return false;
            }
            return true;
        }
        bool CheckRow(int[] row, int[] extracts)
        {
            if (!row.All(i => extracts.Contains(i)))
                return false;
            return true;
        }
        for (int e = 1; e<_extracts.Length; e++)
        {
            foreach (int[][] board in _boards)
            {
                foreach (int colIndex in Enumerable.Range(0, board.GetLength(0)))
                {
                    if (CheckColumn(board, colIndex, _extracts[..e]) || CheckRow(board[colIndex], _extracts[..e]))
                    {
                        return (board.SelectMany(x => x).ToList().Where(i => !_extracts[..e].Contains(i)).Sum() * _extracts[e-1]).ToString();
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
        bool CheckColumn(int[][] board, int column, int[] extracts)
        {
            foreach (int rowIndex in Enumerable.Range(0, board.GetLength(0)))
            {
                if (!extracts.Contains(board[rowIndex][column]))
                    return false;
            }
            return true;
        }
        bool CheckRow(int[] row, int[] extracts)
        {
            if (!row.All(i => extracts.Contains(i)))
                return false;
            return true;
        }
        for (e = 1; e < _extracts.Length; e++)
        {
            for (int x = 0; x < _boards.Count; x++)
            {
                if (!winningBoards.Contains(_boards.IndexOf(_boards[x])))
                {
                    foreach (int colIndex in Enumerable.Range(0, _boards[x].GetLength(0)))
                    {
                        if (CheckColumn(_boards[x], colIndex, _extracts[..e]) || CheckRow(_boards[x][colIndex], _extracts[..e]))
                        {
                            lastWinner = _boards.IndexOf(_boards[x]);
                            extractAtWin = _extracts[e-1];
                            winningBoards.Add(x);
                        }
                    }
                }  
            }
        }
        return (_boards[lastWinner].SelectMany(x => x).ToList().Where(i => !_extracts[..(Array.IndexOf(_extracts, extractAtWin)+1)].Contains(i)).Sum() * extractAtWin).ToString();
    }
}