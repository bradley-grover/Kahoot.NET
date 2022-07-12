using System.Data;
using Flee.PublicTypes;

namespace Kahoot.NET.Parsers;

/// <summary>
/// Calculate the offset for the function to calculate the challenge string
/// </summary>
internal class OffsetArithmetic : IValueParser<long>
{
    private static DataTable Table { get; } = new();
    /// <summary>
    /// Expression context, we use this libary as <see cref="System.Data.DataTable"/> can not
    /// parse some large integer values
    /// </summary>
    private static readonly ExpressionContext context = new();
    public long Parse(ReadOnlySpan<char> input)
    {
        var result = Table.Compute(input.ToString(), null);

        return (long)(int)result;
        //IGenericExpression<long> expression = context.CompileGeneric<long>(input.ToString());

        //return expression.Evaluate();
    }
}
