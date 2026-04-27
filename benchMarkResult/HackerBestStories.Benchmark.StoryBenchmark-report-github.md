```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
AMD Ryzen 7 5825U with Radeon Graphics 2.00GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 10.0.201
  [Host]     : .NET 10.0.5 (10.0.5, 10.0.526.15411), X64 RyuJIT x86-64-v3 [AttachedDebugger]
  DefaultJob : .NET 10.0.5 (10.0.5, 10.0.526.15411), X64 RyuJIT x86-64-v3


```
| Method        | Mean     | Error   | StdDev  | Median   | Allocated |
|-------------- |---------:|--------:|--------:|---------:|----------:|
| GetStories10  | 242.3 ms | 3.36 ms | 2.81 ms | 242.0 ms |   4.32 KB |
| GetStories50  | 245.4 ms | 4.89 ms | 4.34 ms | 243.9 ms |  12.77 KB |
| GetStories100 | 250.0 ms | 4.33 ms | 7.12 ms | 246.1 ms |  23.59 KB |
