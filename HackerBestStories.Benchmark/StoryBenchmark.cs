using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerBestStories.Benchmark
{
    [MemoryDiagnoser]
    public class StoryBenchmark
    {
        private HttpClient _httpClient = null!;
        private const string BaseUrl = "http://localhost:5000";

        [GlobalSetup]
        public void Setup()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _httpClient?.Dispose();
        }

        [Benchmark]
        public async Task GetStories10()
        {
            await _httpClient.GetAsync($"{BaseUrl}/api/stories/10");
        }

        [Benchmark]
        public async Task GetStories50()
        {
            await _httpClient.GetAsync($"{BaseUrl}/api/stories/50");
        }

        [Benchmark]
        public async Task GetStories100()
        {
            await _httpClient.GetAsync($"{BaseUrl}/api/stories/100");
        }
    }
}
