using System.Data;
using System.Text.RegularExpressions;

namespace Kahoot.NET.Parsers;

/*
 * TODO: Faster implementation of calculating offset
 * 
 * .NET 7 adds support for Regex Source Generators so convert code to use that
 * Hopefully span overloads are added as well
 * 
 * The reason this class uses DataTable.Compute instead of a expression parser is because it is way cheaper
 * Flee was used initially but allocates about 10x more memory and about 10x slower as well
 */

/// <summary>
/// Calculate the offset for the function to calculate the challenge string
/// </summary>
internal class OffsetArithmetic : IValueParser<long>
{
    // TODO: find better expression for matching
    private static Regex InternalRegex { get; } = new(@"\d+(\.\d+)?", RegexOptions.Compiled);
    private static DataTable Table { get; } = new();

    public long Parse(ReadOnlySpan<char> input)
    {
        var result = Table.Compute(SanitiseToDecimal(input).ToString(), null);

        return Convert.ToInt64((decimal)result);
    }

    internal static ReadOnlySpan<char> SanitiseToDecimal(ReadOnlySpan<char> value)
    {
        return InternalRegex.Replace(value.ToString(), m =>
        {
            var slice = m.ValueSpan;
            return slice.Contains('.') ? slice.ToString() : string.Format("{0}.0", slice.ToString());
        });
    }
}
