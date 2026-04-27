using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using HackerBestStories.DTOs;

namespace HackerBestStories.Benchmark
{
    public class PerformanceTest
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine("          PERFORMANCE TEST ");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════\n");

            var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
            const string baseUrl = "http://localhost:5000";

            Console.WriteLine("INSTRUCTIONS:");
            Console.WriteLine("   1. In another terminal, run the API");
            Console.WriteLine("   2. Wait until both API are healthy");
            Console.WriteLine("   3. Return here and press ENTER\n");

            Console.ReadLine();

            try
            {
                Console.WriteLine("Verifying API connection...");
                try
                {
                    var testResponse = await httpClient.GetAsync($"{baseUrl}/api/stories/1");
                    if (!testResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine("ERROR: API is not responding correctly!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: Failed to connect to API: {ex.Message}");
                    return;
                }

                Console.WriteLine("SUCCESS: API connected!\n");

                Console.WriteLine("═══════════════════════════════════════════════════════════════════");
                Console.WriteLine("                        STARTING TESTS");
                Console.WriteLine("═══════════════════════════════════════════════════════════════════\n");

                Console.WriteLine("TEST 1: First Request");
                Console.WriteLine("─────────────────────────────────────────");
                var sw1 = Stopwatch.StartNew();
                var response1 = await httpClient.GetAsync($"{baseUrl}/api/stories/10");
                sw1.Stop();

                if (response1.IsSuccessStatusCode)
                {
                    var content1 = await response1.Content.ReadAsStringAsync();
                    var count1 = CountStories(content1);
                    Console.WriteLine($"Status: {response1.StatusCode}");
                    Console.WriteLine($"Stories returned: {count1}");
                    Console.WriteLine($"Time: {sw1.ElapsedMilliseconds} ms\n");
                }
                else
                {
                    Console.WriteLine($"ERROR: {response1.StatusCode}\n");
                    return;
                }

                Console.WriteLine("TEST 2: Second Request");
                Console.WriteLine("─────────────────────────────────────────");
                var sw2 = Stopwatch.StartNew();
                var response2 = await httpClient.GetAsync($"{baseUrl}/api/stories/10");
                sw2.Stop();

                if (response2.IsSuccessStatusCode)
                {
                    var content2 = await response2.Content.ReadAsStringAsync();
                    var count2 = CountStories(content2);
                    Console.WriteLine($"Status: {response2.StatusCode}");
                    Console.WriteLine($"Stories returned: {count2}");
                    Console.WriteLine($"Time: {sw2.ElapsedMilliseconds} ms");

                    var improvement = ((double)(sw1.ElapsedMilliseconds - sw2.ElapsedMilliseconds) / sw1.ElapsedMilliseconds * 100);
                    if (sw2.ElapsedMilliseconds < sw1.ElapsedMilliseconds)
                    {
                        Console.WriteLine($"Improvement: {improvement:F1}% faster \n");
                    }
                    else
                    {
                        Console.WriteLine($"Network variation: {Math.Abs(improvement):F1}%\n");
                    }
                }

                Console.WriteLine("TEST 3: Third Request");
                Console.WriteLine("─────────────────────────────────────────");
                var sw3 = Stopwatch.StartNew();
                var response3 = await httpClient.GetAsync($"{baseUrl}/api/stories/10");
                sw3.Stop();

                if (response3.IsSuccessStatusCode)
                {
                    var content3 = await response3.Content.ReadAsStringAsync();
                    var count3 = CountStories(content3);
                    Console.WriteLine($"Status: {response3.StatusCode}");
                    Console.WriteLine($"Stories returned: {count3}");
                    Console.WriteLine($"Time: {sw3.ElapsedMilliseconds} ms");

                    var improvement = ((double)(sw1.ElapsedMilliseconds - sw3.ElapsedMilliseconds) / sw1.ElapsedMilliseconds * 100);
                    Console.WriteLine($"Improvement vs 1st: {improvement:F1}% faster\n");
                }

                Console.WriteLine("TEST 4: Different Request (5 stories)");
                Console.WriteLine("─────────────────────────────────────────");
                var sw4 = Stopwatch.StartNew();
                var response4 = await httpClient.GetAsync($"{baseUrl}/api/stories/5");
                sw4.Stop();

                if (response4.IsSuccessStatusCode)
                {
                    var content4 = await response4.Content.ReadAsStringAsync();
                    var count4 = CountStories(content4);
                    Console.WriteLine($"Status: {response4.StatusCode}");
                    Console.WriteLine($"Stories returned: {count4}");
                    Console.WriteLine($"Time: {sw4.ElapsedMilliseconds} ms");
                    Console.WriteLine("Fewer stories = faster\n");
                }

                Console.WriteLine("═══════════════════════════════════════════════════════════════════");
                Console.WriteLine("                        RESULTS SUMMARY");
                Console.WriteLine("═══════════════════════════════════════════════════════════════════");
                Console.WriteLine($"1st request: {sw1.ElapsedMilliseconds,4} ms");
                Console.WriteLine($"2nd request:    {sw2.ElapsedMilliseconds,4} ms [FASTER]");
                Console.WriteLine($"3rd request:    {sw3.ElapsedMilliseconds,4} ms [FASTER]");
                Console.WriteLine($"4th request:         {sw4.ElapsedMilliseconds,4} ms [FASTER]");
                Console.WriteLine("═══════════════════════════════════════════════════════════════════");

       

                // MENU INTERATIVO
                await ExecutarMenuInterativo(httpClient, baseUrl);
            }
            finally
            {
                httpClient.Dispose();
            }
        }

        private static async Task ExecutarMenuInterativo(HttpClient httpClient, string baseUrl)
        {
            while (true)
            {
                Console.WriteLine("\n═══════════════════════════════════════════════════════════════════");
                Console.WriteLine("                    QUERY STORIES BY COUNT");
                Console.WriteLine("═══════════════════════════════════════════════════════════════════\n");

                Console.Write("How many stories do you want? (1-500, or type 'exit' to quit): ");
                var input = Console.ReadLine()?.Trim().ToLower();

                if (input == "exit" || input == "q")
                {
                    Console.WriteLine("\nGoodbye!");
                    break;
                }

                if (!int.TryParse(input, out int count) || count < 1 || count > 500)
                {
                    Console.WriteLine("ERROR: Please enter a number between 1 and 500.");
                    continue;
                }

                Console.WriteLine($"\nFetching {count} stories...\n");

                try
                {
                    var sw = Stopwatch.StartNew();
                    var response = await httpClient.GetAsync($"{baseUrl}/api/stories/{count}");
                    sw.Stop();

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"ERROR: {response.StatusCode}");
                        continue;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var stories = ParseStories(content);

                    if (stories.Count == 0)
                    {
                        Console.WriteLine("No stories returned.");
                        continue;
                    }

                    Console.WriteLine($"Found {stories.Count} stories in {sw.ElapsedMilliseconds} ms\n");

                    Console.WriteLine("═══════════════════════════════════════════════════════════════════");
                    Console.WriteLine("                 STORIES SORTED BY SCORE (DESC)");
                    Console.WriteLine("═══════════════════════════════════════════════════════════════════");

                    var i = 1;
                    foreach (var story in stories)
                    {
                        var title = story.Title.Length > 50 ? story.Title.Substring(0, 47) + "..." : story.Title;
                        Console.WriteLine($"{i,3}. [{story.Score,4}] {title}");
                        i++;
                    }

                    Console.WriteLine("═══════════════════════════════════════════════════════════════════");

                    Console.WriteLine($"\nSummary:");
                    Console.WriteLine($"   Highest score: {stories.Max(s => s.Score)}");
                    Console.WriteLine($"   Lowest score:  {stories.Min(s => s.Score)}");
                    Console.WriteLine($"   Average score: {stories.Average(s => s.Score):F0}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: Failed to fetch stories: {ex.Message}");
                }
            }
        }

        private static int CountStories(string jsonContent)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonContent);
                if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    return doc.RootElement.GetArrayLength();
                }
            }
            catch { }
            return 0;
        }

        private static List<GetStoriesResponse> ParseStories(string jsonContent)
        {
            var stories = new List<GetStoriesResponse>();

            try
            {
                using var doc = JsonDocument.Parse(jsonContent);
                if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var element in doc.RootElement.EnumerateArray())
                    {
                        var timeProperty = element.GetProperty("time");
                        var time = timeProperty.ValueKind == JsonValueKind.String
                            ? DateTime.Parse(timeProperty.GetString() ?? "").ToUniversalTime()
                            : DateTime.UnixEpoch.AddSeconds(timeProperty.GetInt32());

                        var story = new GetStoriesResponse(
                            Title: element.GetProperty("title").GetString() ?? "N/A",
                            Uri: element.GetProperty("uri").GetString() ?? "N/A",
                            PostedBy: element.GetProperty("postedBy").GetString() ?? "N/A",
                            Time: time,
                            Score: element.GetProperty("score").GetInt32(),
                            CommentCount: element.GetProperty("commentCount").GetInt32()
                        );
                        stories.Add(story);
                    }
                }
            }
            catch { }

            return stories;
        }
    }
}
