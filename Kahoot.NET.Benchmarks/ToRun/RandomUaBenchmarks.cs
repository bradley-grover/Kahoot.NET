using System.Diagnostics.CodeAnalysis;

namespace Kahoot.NET.Benchmarks.ToRun;

[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class RandomUaBenchmarks
{
    [Benchmark]
    public void Kahoot()
    {
        _ = RandomUserAgent.UserAgent.Generate();
    }

    [Benchmark]
    public void RandomUserAgentNuGet()
    {
        _ = global::RandomUserAgent.RandomUa.RandomUserAgent; // refer to global namespace
    }
}
