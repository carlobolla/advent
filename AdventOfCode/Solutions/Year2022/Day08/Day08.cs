using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day08 : ASolution
{
    private record Direction(int DRow, int DCol);
    private record Tree(int Height, int X, int Y);
    private static readonly Direction Left = new(0, -1);
    private static readonly Direction Right = new(0, 1);
    private static readonly Direction Up = new(-1, 0);
    private static readonly Direction Down = new(1, 0);
    
    private IEnumerable<Tree> TreesInDirection(Tree tree, Direction dir) {
        var (first, row, col) = (true, tree.X, tree.Y);
        while (row >= 0 && row < _lines[0].Length && col >= 0 && col < _lines.Length) {
            if (!first) {
                yield return new Tree(_lines[row][col], row, col);
            }
            (first, row, col) = (false, row + dir.DRow, col + dir.DCol);
        }
    }

    private int ViewDistance(Tree tree, Direction dir)
    {
        var trees = TreesInDirection(tree, dir).ToList();
        return trees.All(treeT => treeT.Height < tree.Height) ?
            trees.Count :
            trees.TakeWhile(treeT => treeT.Height < tree.Height).Count() + 1;
    }

    private static IEnumerable<Tree> GetForest(IReadOnlyList<string> lines)
    {
        return Enumerable.Range(0, lines.Count)
            .SelectMany(row => Enumerable.Range(0, lines[row].Length)
                .Select(col => new Tree(lines[row][col], row, col)));
    }

    private readonly string[] _lines;
    public Day08() : base(08, 2022, "Treetop Tree House")
    {
        _lines = Input.SplitByNewline();
    }

    protected override string SolvePartOne()
    {
        var trees = GetForest(Input.SplitByNewline());
        return trees.Count(tree =>
            TreesInDirection(tree, Left).All(treeT => treeT.Height < tree.Height) ||
            TreesInDirection(tree, Right).All(treeT => treeT.Height < tree.Height) ||
            TreesInDirection(tree, Up).All(treeT => treeT.Height < tree.Height) ||
            TreesInDirection(tree, Down).All(treeT => treeT.Height < tree.Height)
        ).ToString();
    }

    protected override string SolvePartTwo()
    {
        var trees = GetForest(Input.SplitByNewline());
        return trees.Max(tree => 
            ViewDistance(tree, Left) *
            ViewDistance(tree, Right) *
            ViewDistance(tree, Up) *
            ViewDistance(tree, Down)).ToString();
    }
}
