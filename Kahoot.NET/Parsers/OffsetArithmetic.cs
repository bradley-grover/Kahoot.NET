using System.Data;

namespace Kahoot.NET.Parsers;

/// <summary>
/// Calculate the offset for the function to calculate the challenge string
/// </summary>
internal class OffsetArithmetic : IValueParser<long>
{
    private static DataTable Table { get; } = new();

    public long Parse(ReadOnlySpan<char> input)
    {
        var result = Table.Compute(input.ToString(), null);

        return (long)(int)result;
    }
}
