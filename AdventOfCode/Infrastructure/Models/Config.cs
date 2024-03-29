using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AdventOfCode.Infrastructure.Models;

internal class Config
{
    private string _c;
    private int _y;
    private int[] _d;

    public string Cookie
    {
        get => _c;
        set
        {
            if (Regex.IsMatch(value, "^session=[a-z0-9]+$")) _c = value;
        }
    }
    public int Year
    {
        get => _y;
        set
        {
            if (value >= 2015 && value <= DateTime.Now.Year) _y = value;
        }
    }
    [JsonConverter(typeof(DaysConverter))]
    public int[] Days
    {
        get => _d;
        set
        {
            bool allDaysCovered = false;
            _d = value.Where(v =>
            {
                if (v == 0) allDaysCovered = true;
                return v > 0 && v < 26;
            }).ToArray();

            if (allDaysCovered)
            {
                _d = new int[] { 0 };
            }
            else
            {
                Array.Sort(_d);
            }
        }
    }

    private void SetDefaults()
    {
        //Make sure we're looking at EST, or it might break for most of the US
        DateTime currentEst = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
        if (Cookie == default) Cookie = "";
        if (Year == default) Year = currentEst.Year;
        if (Days == default(int[])) Days = (currentEst.Month == 12 && currentEst.Day <= 25) ? new int[] { currentEst.Day } : new int[] { 0 };
    }

    public static Config Get(string path)
    {
        var options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        Config config;
        if (File.Exists(path))
        {
            config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path), options);
            config.SetDefaults();
        }
        else
        {
            config = new Config();
            config.SetDefaults();
            File.WriteAllText(path, JsonSerializer.Serialize<Config>(config, options));
        }

        return config;
    }
}

internal class DaysConverter : JsonConverter<int[]>
{
    public override int[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number) return new int[] { reader.GetInt16() };
        var tokens = reader.TokenType == JsonTokenType.String
            ? new string[] { reader.GetString() }
            : JsonSerializer.Deserialize<object[]>(ref reader).Select<object, string>(o => o.ToString());
        return tokens.SelectMany<string, int>(ParseString).ToArray();
    }

    private IEnumerable<int> ParseString(string str)
    {
        return str.Split(",").SelectMany<string, int>(str =>
        {
            if (str.Contains(".."))
            {
                var split = str.Split("..");
                int start = int.Parse(split[0]);
                int stop = int.Parse(split[1]);
                return Enumerable.Range(start, stop - start + 1);
            }
            else if (int.TryParse(str, out int day))
            {
                return new int[] { day };
            }

            return Array.Empty<int>();
        });
    }

    public override void Write(Utf8JsonWriter writer, int[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (int val in value) writer.WriteNumberValue(val);
        writer.WriteEndArray();
    }
}