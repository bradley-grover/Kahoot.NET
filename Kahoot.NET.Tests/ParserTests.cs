using Kahoot.NET.Client;
using Kahoot.NET.Parsers;

namespace Kahoot.NET.Tests;

[Trait(Traits.Parsers, Traits.ParsersDesc)]
public class ParserTests
{
    [Theory(DisplayName = "Parse Expression Test")]
    [InlineData("2*2", 4)]
    [InlineData("2+2", 4)]
    [InlineData("(2*2)*4", 16)]
    [InlineData("((2*5)+(2*2))", 14)]
    [InlineData("(60*20*50)+(20*2)", 60_040)]
    [InlineData("(90*30*68)*(80*80*20*30)", 705024000000)]
    [InlineData("(90*90*90*90*90)+(10*8)", 5_904_900_080)]
    public void AssertThatParser_Works(string expression, long expected)
    {
        IValueParser<long> parser = new OffsetArithmetic();
        
        Assert.Equal(expected, parser.Parse(expression));
    }

}
