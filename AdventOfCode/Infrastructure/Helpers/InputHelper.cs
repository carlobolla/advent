using System;
using System.IO;
using System.Net;
using System.Net.Http;
using AdventOfCode.Infrastructure.Models;

namespace AdventOfCode.Infrastructure.Helpers;

internal static class InputHelper
{
    private readonly static string Cookie = Config.Get("config.json").Cookie;

    public static string LoadInput(int day, int year)
    {
        string inputFilepath = GetDayPath(day, year) + "/input";
        string inputUrl = GetAocInputUrl(day, year);
        string input = "";

        if (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
        {
            input = File.ReadAllText(inputFilepath);
        }
        else
        {
            try
            {
                DateTime currentEst = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
                if (currentEst < new DateTime(year, 12, day)) throw new InvalidOperationException();

                using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
                {
                    client.DefaultRequestHeaders.Add("Cookie", Cookie);
                    input = client.GetStringAsync(inputUrl).Result;
                    File.WriteAllText(inputFilepath, input);
                }
            }
            catch (HttpRequestException e)
            {
                var statusCode = e.StatusCode;
                var colour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (statusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Day {day}: Received 400 when attempting to retrieve puzzle input. Your session cookie is probably not recognized.");

                }
                else if (statusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Day {day}: Received 404 when attempting to retrieve puzzle input. The puzzle is probably not available yet.");
                }
                else
                {
                    Console.ForegroundColor = colour;
                    Console.WriteLine(e.ToString());
                }
                Console.ForegroundColor = colour;
            }
            catch (InvalidOperationException)
            {
                var colour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Day {day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
                Console.ForegroundColor = colour;
            }
        }
        return input;
    }

    public static string LoadDebugInput(int day, int year)
    {
        string inputFilepath = GetDayPath(day, year) + "/debug";
        return (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
            ? File.ReadAllText(inputFilepath)
            : "";
    }

    private static string GetDayPath(int day, int year)
        => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../Solutions/Year{year}/Day{day.ToString("D2")}"));

    private static string GetAocInputUrl(int day, int year)
        => $"https://adventofcode.com/{year}/day/{day}/input";
}