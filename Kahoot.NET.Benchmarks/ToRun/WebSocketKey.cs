using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Kahoot.NET.API.Authentication;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.PortableExecutable;

namespace Kahoot.NET.Benchmarks.ToRun;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[SimpleJob(RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net60, targetCount: 50)]
[SimpleJob(RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net70, targetCount: 50)]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class WebSocketKeyBenchmarks
{
    [Benchmark]
    public void Key_Create()
    {
        _ = WebSocketKey.Create(Mock.SessionHeader, Mock.ChallengeFunction);
    }

    [Benchmark]
    public void Get_Challenge()
    {
        /// from WebSocketKey.Create(str, str)

        Span<char> challenge = stackalloc char[512];

        int challengeWritten = WebSocketKey.GetChallenge(Mock.ChallengeFunction, challenge);

        challenge = challenge[..challengeWritten];
    }

    [Benchmark]
    public void Get_Header()
    {
        // from WebSoccketKey.Create(str, str);

        Span<char> header = stackalloc char[512];

        int written = WebSocketKey.GetHeader(Mock.SessionHeader, header);

        header = header[..written];
    }
}
