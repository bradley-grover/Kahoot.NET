using Kahoot.NET.Internals.Parsers;
using Xunit;

namespace Kahoot.NET.Tests;


public class ParserTests
{
    [Fact]
    public void AssertThatParser_Works()
    {
        IValueParser<long> parser = new LargeOffsetParser();
        Assert.Equal(4, parser.Parse("2+2"));
    }
}
