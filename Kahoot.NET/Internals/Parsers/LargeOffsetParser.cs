using Towel.Mathematics;
using Flee;
using Flee.PublicTypes;

namespace Kahoot.NET.Internals.Parsers;

internal class LargeOffsetParser : IValueParser<long>
{
    static readonly ExpressionContext context = new();

    public long Parse(ReadOnlySpan<char> input)
    {
        IGenericExpression<long> expression = context.CompileGeneric<long>(input.ToString());

        return expression.Evaluate();
    }
}
