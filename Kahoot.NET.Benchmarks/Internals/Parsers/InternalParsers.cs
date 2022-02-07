using Kahoot.NET.Internals.Parsers;

namespace Kahoot.NET.Benchmarks.Internals.Parsers;

// note that this benchmark is not a comparison rather just the same category

[MemoryDiagnoser]
public class InternalParsers
{
    public static readonly string ForOffset =
        "jasfsadlfajfsakf s a varoffset=(5+2)/2*2; ok";
    public static readonly string ForToken =
        "najfnasfkjanfaslnf'jfuhfa8ewfhw8efae8aefuiasehfsaiufsa'asd;fjasf";
    // 2022-02-05 | 40.88ns | 24 bytes
    [Benchmark]
    public void OffsetParserTest()
    {
        IParser<char> parser = new OffsetParser();
        var parse = parser.Parse(ForOffset);
    }
    [Benchmark]
    public void OffsetParserInString()
    {
        var parser = new StringOffsetParser();
        var parse = parser.Parse(ForOffset);
    }
    // 2022-02-05 | 20.01 ns | 0 bytes
    [Benchmark]
    public void TokenParserTest()
    {
        IParser<char> parser = new SpecialParameterParser();
        var parse = parser.Parse(ForToken);
    }
}
