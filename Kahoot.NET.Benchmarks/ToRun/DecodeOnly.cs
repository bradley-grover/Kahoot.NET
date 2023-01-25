using Kahoot.NET.API.Authentication;

namespace Kahoot.NET.Benchmarks.ToRun;

public class DecodeOnly
{
    [Benchmark]
    public void Decode()
    {
        Span<char> buffer = stackalloc char[256];

        _ = Challenge.Decode(Mock.Token, buffer, Mock.Offset);
    }
}
