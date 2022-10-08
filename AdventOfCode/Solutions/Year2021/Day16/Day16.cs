using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021;

internal class Day16 : ASolution
{
    private readonly Queue<char> _binaryCode;
    private Packet _mainPacket;
    public Day16() : base(16, 2021, "Packet Decoder")
    {
        _binaryCode = new Queue<char>(Input.Aggregate("",
            (current, c) => current + Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
    }

    protected override string SolvePartOne()
    {
        _mainPacket = GetPacket(_binaryCode);
        return (_mainPacket.Version + GetAllChildren(_mainPacket).Sum(packet => packet.Version)).ToString();
    }
    protected override string SolvePartTwo()
    {
        return (_mainPacket.Result).ToString();
    }
    
    private static IEnumerable<Packet> GetAllChildren(Packet packet)
    {
        return packet.Subpackets!.Concat(packet.Subpackets!.SelectMany(GetAllChildren));
    }
    private static int GetNumberFromBinary(Queue<char> binary, int count)
    {
        return Convert.ToInt32(GetBitsFromBinary(binary, count), 2);
    }
    private static string GetBitsFromBinary(Queue<char> binary, int count)
    {
        return string.Concat(Enumerable.Range(0, count).Select(_ => binary.Dequeue()));
    }

    private static Packet GetPacket(Queue<char> binary)
    {
        var packet = new Packet
        {
            Raw = GetBitsFromBinary(binary, 3)
        };
        packet.Version = Convert.ToInt32(packet.Raw, 2);
        string typeId = GetBitsFromBinary(binary, 3);
        packet.Raw += typeId;
        packet.TypeId = Convert.ToInt32(typeId, 2);
        if (packet.TypeId != 4)
        {
            packet.LengthTypeId = GetNumberFromBinary(binary, 1);
            packet.Raw += packet.LengthTypeId.ToString();
            string length = packet.LengthTypeId == 0 ? GetBitsFromBinary(binary, 15) : GetBitsFromBinary(binary, 11);
            packet.Raw += length;
            packet.Length = Convert.ToInt32(length, 2);
            packet.Subpackets = new List<Packet>();
            if (packet.LengthTypeId == 0)
            {
                while (packet.Subpackets.Sum(p => p.Raw.Length) < packet.Length)
                {
                    packet.Subpackets.Add(GetPacket(binary));
                    packet.Raw += packet.Subpackets.Last().Raw;
                }
            }
            else
            {
                while (packet.Subpackets.Count < packet.Length)
                {
                    packet.Subpackets.Add(GetPacket(binary));
                    packet.Raw += packet.Subpackets.Last().Raw;
                }
            }
        }
        else
        {
            string sub;
            do
            {
                sub = GetBitsFromBinary(binary, 5);
                packet.Raw += sub;
                packet.Literal += sub[1..];
            } while (sub.StartsWith('1'));
        }
        return packet;
    }
}

internal class Packet
{
    public int Version;
    public int TypeId;
    public bool IsOperator => TypeId != 4;
    public int? LengthTypeId;
    public int? Length;
    public string Literal = "";
    public List<Packet> Subpackets = new();
    public string Raw;
    public long Result {
        get
        {
            switch (TypeId)
            {
                case 0:
                    return Subpackets.Sum(p => p.Result);
                case 1:
                    long prod = Subpackets.Select(p => p.Result).Aggregate(1L, (current, value) => current * value);
                    return prod;
                case 2:
                    return Subpackets.Min(p => p.Result);
                case 3:
                    return Subpackets.Max(p => p.Result);
                case 4:
                    return Convert.ToInt64(Literal, 2);
                case 5:
                    return Subpackets[0].Result > Subpackets[1].Result ? 1 : 0;
                case 6:
                    return Subpackets[0].Result < Subpackets[1].Result ? 1 : 0;
                case 7:
                    return Subpackets[0].Result == Subpackets[1].Result ? 1 : 0;
            }
            return 0;
        }
    }
}

