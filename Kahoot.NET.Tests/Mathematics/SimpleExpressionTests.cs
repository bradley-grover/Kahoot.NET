using Kahoot.NET.Mathematics;

namespace Kahoot.NET.Tests.Mathematics;

public class SimpleExpressionTests
{
    [Theory]
    [InlineData("2*2", 4)]
    [InlineData("2+2", 4)]
    [InlineData("(2*2)*4", 16)]
    [InlineData("((2*5)+(2*2))", 14)]
    [InlineData("(60*20*50)+(20*2)", 60_040)]
    [InlineData("(90*30*68)*(80*80*20*30)", 705024000000)]
    [InlineData("(90*90*90*90*90)+(10*8)", 5_904_900_080)]
    public void Assert_SimpleExpressionWorks(string input, double expected)
    {
        var result = SimpleExpression.Evaluate(input);

        Assert.Equal(expected, result);
    }
}
