using Kahoot.NET.Mathematics;

namespace Kahoot.NET.Tests.Mathematics;

public class SimpleExpressionTests
{
    [Theory]
    [ClassData(typeof(Expressions))]
    public void Assert_SimpleExpressionWorks(string input, decimal expected)
    {
        var result = SimpleExpression.Evaluate(input);

        long expectedValue = (long)expected;

        Assert.Equal(expectedValue, result);
    }
}
