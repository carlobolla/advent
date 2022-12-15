using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Linq;

namespace AdventOfCode.Solutions.Year2022;

internal class Day13 : ASolution
{
    public Day13() : base(13, 2022, "Distress Signal")
    {
    }

    private static IEnumerable<JsonNode> GetPackets(string input)
    {
        return input.SplitByNewline().Where(l => !string.IsNullOrEmpty(l)).Select(l => JsonNode.Parse(l))!; 
    }
    
    private static int Compare(JsonNode a, JsonNode b)
    {
        if (a is JsonValue && b is JsonValue)
            return (int)a - (int)b;
        var arrayA = a as JsonArray ?? new JsonArray((int)a);
        var arrayB = b as JsonArray ?? new JsonArray((int)b);
        return arrayA.Zip(arrayB).Select(p => Compare(p.First!, p.Second!)).FirstOrDefault(c => c != 0, arrayA.Count - arrayB.Count);
    }

    protected override string SolvePartOne()
    {
        return GetPackets(Input).Chunk(2).Select((pair, index) => Compare(pair[0], pair[1]) < 0 ? index + 1 : 0).Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        var dividerPackets = GetPackets("[[2]]\n[[6]]").ToList();
        var packets = GetPackets(Input).Concat(dividerPackets).ToList();
        packets.Sort(Compare);
        return ((packets.IndexOf(dividerPackets[0]) + 1) * (packets.IndexOf(dividerPackets[1]) + 1)).ToString();
    }
}
