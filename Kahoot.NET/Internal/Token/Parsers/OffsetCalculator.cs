using Flee.PublicTypes;

namespace Kahoot.NET.Internal.Token.Parsers;

/// <summary>
/// Calculate the offset for the function to calculate the challenge string
/// </summary>
internal class OffsetCalculator : IValueParser<long>
{
    /// <summary>
    /// Expression context, we use this libary as <see cref="System.Data.DataTable"/> can not
    /// parse some large integer values
    /// </summary>
    private static readonly ExpressionContext context = new();
    public long Parse(ReadOnlySpan<char> input)
    {
        IGenericExpression<long> expression = context.CompileGeneric<long>(input.ToString());

        return expression.Evaluate();
    }
}
