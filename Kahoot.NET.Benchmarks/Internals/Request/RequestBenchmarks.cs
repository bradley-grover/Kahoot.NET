using Kahoot.NET.Internal.Token;

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
        await Token.CreateTokenAndSessionAsync(2162040, new());
    }
}
