using Kahoot.NET.Internal.Token.Parsers;

namespace Kahoot.NET.Tests;

public class ParserTests
{
    [Theory(DisplayName = "Parse Expression Test")]
    [InlineData("2*2", 4)]
    [InlineData("2+2", 4)]
    [InlineData("(2*2)*4", 16)]
    [InlineData("(50/5)*2+5", 25)]
    [InlineData("5*5/25+16", 17)]
    public void AssertThatParser_Works(string expression, long expected)
    {
        IValueParser<long> parser = new OffsetCalculator();
        
        Assert.Equal(expected, parser.Parse(expression));
    }
}
