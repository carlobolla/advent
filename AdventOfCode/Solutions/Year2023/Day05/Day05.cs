using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023
{
    internal class Day05 : ASolution
    {
        private readonly List<string> lines;
        private const string SEED_TO_SOIL = "seed-to-soil map:";
        private const string SOIL_TO_FERTILIZER = "soil-to-fertilizer map:";
        private const string FERTILIZER_TO_WATER = "fertilizer-to-water map:";
        private const string WATER_TO_LIGHT = "water-to-light map:";
        private const string LIGHT_TO_TEMPERATURE = "light-to-temperature map:";
        private const string TEMPERATURE_TO_HUMIDITY = "temperature-to-humidity map:";
        private const string HUMIDITY_TO_LOCATION = "humidity-to-location map:";
        
        public Day05() : base(05, 2023, "If You Give A Seed A Fertilizer")
        {
            lines = Input.SplitByNewline().ToList();
        }

        private record Range(long destination, long source, long rangeLength);

        private List<Range> getRanges(string from, string to)
        {
            var a = lines.SkipWhile(l => l != from).TakeWhile(l => l != to).Skip(1).Select(r =>
            {
                var splits = r.Split(" ").Select(long.Parse).ToList();
                return new Range(splits[0], splits[1], splits[2]);
            }).ToList();
            return a;
        }

        private List<long> getSeeds()
        {
            return Regex.Matches(lines[0], @" (\d*)").Select(m => long.Parse(m.Value)).ToList();
        }

        private static long ConvertIfInRange(long test, Range range)
        {
            return test < (range.source + range.rangeLength) && test >= range.source
                ? test + range.destination - range.source
                : -1;
        }
        
        protected override string SolvePartOne()
        {
            var seeds = getSeeds();
            var soil = seeds.Select(s =>
                getRanges(SEED_TO_SOIL, SOIL_TO_FERTILIZER).Select(r => ConvertIfInRange(s, r))
                    .FirstOrDefault(d => d != -1, s));
            var fertilizer = soil.Select(s =>
                getRanges(SOIL_TO_FERTILIZER, FERTILIZER_TO_WATER).Select(r => ConvertIfInRange(s, r))
                    .FirstOrDefault(d => d != -1, s));
            var water = fertilizer.Select(s =>
                getRanges(FERTILIZER_TO_WATER, WATER_TO_LIGHT).Select(r => ConvertIfInRange(s, r))
                    .FirstOrDefault(d => d != -1, s));
            var light = water.Select(s =>
                getRanges(WATER_TO_LIGHT, LIGHT_TO_TEMPERATURE).Select(r => ConvertIfInRange(s, r))
                    .FirstOrDefault(d => d != -1, s));
            var temperature = light.Select(s =>
                getRanges(LIGHT_TO_TEMPERATURE, TEMPERATURE_TO_HUMIDITY).Select(r => ConvertIfInRange(s, r))
                    .FirstOrDefault(d => d != -1, s));
            var humidity = temperature.Select(s =>
                getRanges(TEMPERATURE_TO_HUMIDITY, HUMIDITY_TO_LOCATION).Select(r => ConvertIfInRange(s, r))
                    .FirstOrDefault(d => d != -1, s));
            var location = humidity.Select(s =>
                getRanges(HUMIDITY_TO_LOCATION, "").Select(r => ConvertIfInRange(s, r))
                    .FirstOrDefault(d => d != -1, s));
            return location.Min().ToString();
        }

        protected override string SolvePartTwo()
        {
            return "";
        }
    }
}