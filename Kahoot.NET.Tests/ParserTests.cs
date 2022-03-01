using Kahoot.NET.Internal.Token.Parsers;
using Xunit;

namespace Kahoot.NET.Tests;


public class ParserTests
{
    [Fact]
    public void AssertThatParser_Works()
    {
        IValueParser<long> parser = new OffsetCalculator();
        Assert.Equal(4, parser.Parse("2+2"));
    }
}
