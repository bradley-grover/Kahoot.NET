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

#if NET7_0_OR_GREATER
    [Theory]
    [ClassData(typeof(Expressions))]
    public void Assert_SimpleExpressionWorks_Generic_OnlyNet7G(string input, decimal expected)
    {
        var result = SimpleExpression<long>.Evaluate(input);

        var expectedValue = long.CreateChecked(expected);

        Assert.Equal(expectedValue, result);
    }
#endif
}
