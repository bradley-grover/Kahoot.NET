using Flee.PublicTypes;

namespace Kahoot.NET.Internal.Token.Parsers;

internal class OffsetCalculator : IValueParser<long>
{
    private static readonly ExpressionContext context = new();
    public long Parse(ReadOnlySpan<char> input)
    {
        IGenericExpression<long> expression = context.CompileGeneric<long>(input.ToString());

        return expression.Evaluate();
    }
}
