using Kahoot.NET.Internals.Connection.Token;

namespace Kahoot.NET.Benchmarks.Internals.Request;

[MemoryDiagnoser]
public class RequestBenchmarks
{
    /// <summary>
    /// Set this before running the benchmark
    /// </summary>
    [Benchmark]
    public async Task CreateRequest()
    {
        await Token.CreateTokenSessionAsync(2162040, new());
    }
}
