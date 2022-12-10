using System.Diagnostics.CodeAnalysis;
using Kahoot.NET.API.Authentication;

namespace Kahoot.NET.Benchmarks.ToRun;

[BenchmarkModule("DecodeBenchmark", "For only the decode part of the challenge solving")]
public class DecodeOnly
{
    [Benchmark]
    public void Decode()
    {
        Span<char> buffer = stackalloc char[256];

        _ = Challenge.Decode(Mock.Token, buffer, Mock.Offset);
    }
}
