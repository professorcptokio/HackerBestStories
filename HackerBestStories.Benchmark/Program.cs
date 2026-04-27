using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Running;
using HackerBestStories.Benchmark;

var config = DefaultConfig.Instance
    .AddExporter(JsonExporter.Full)           // JSON
    .AddExporter(MarkdownExporter.GitHub)     // Markdown
    .AddExporter(HtmlExporter.Default)        // HTML
    .AddExporter(CsvExporter.Default);        // CSV

var summary = BenchmarkRunner.Run<StoryBenchmark>(config);

await PerformanceTest.Main(args);