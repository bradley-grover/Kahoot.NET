using Kahoot.NET.Internals.Connection.Token;
using Kahoot.NET.Shared;

namespace Kahoot.NET.Benchmarks.Shared;
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable IDE0059 // Unnecessary assignment of a value
[MemoryDiagnoser]
public class SharedBenchmarks
{
    private const string Data = 
        "a is o fjeo3 q7fh 8yqh q39qhf qew 837 q 49q yfh7 qf 9 q4 8f 4h f4  qh f7 4q8 whqf8f";

    // 2022-02-04 | 787.7ns | 848 bytes
    [Benchmark(Baseline = true)]
    public void RemoveWhitespaceString()
    {
        string item = Data;
#pragma warning disable CS0618 // Type or member is obsolete
        item = item.RemoveWhitespace();
#pragma warning restore CS0618 // Type or member is obsolete
    }

    // 2022-02-04 | 405ns 152 bytes
    [Benchmark]
    public void RemoveWhitespaceSpan()
    {
        ReadOnlySpan<char> item = Data;
        item = item.RemoveWhitespace();
    }

    [Benchmark]
    public void RemoveWhitespaceFixed()
    {
        string item = Data;
        item = item.RemoveWhitespaceM();
    }
    [Benchmark]
    public void RemoveWhitespaceInPlaceCharArray()
    {
        string item = Data;
        item = item.RemoveInPlaceCharArray();
    }
}
