using Kahoot.NET.Client;

namespace Kahoot.NET.Benchmarks.Internals.Handshake;

[MemoryDiagnoser]
public class HandshakeBenchmarks
{
    [Benchmark]
    public void CreateHandshake()
    {
        var client = new KahootClient(KahootClientConfig.Default, new HttpClient());
    }
}
