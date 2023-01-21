using System.Data;
using System.Text.RegularExpressions;

namespace Kahoot.NET.Benchmarks.Alternatives;

internal static partial class MathParserAlternatives
{
    // Uses regex source generator in .NET 7 instead
    private static Regex InternalRegex { get; } =
#if NET7_0_OR_GREATER
        GenerateRegex();

        [GeneratedRegex(@"\d+(\.\d+)?")]
        internal static partial Regex GenerateRegex();
#else
        new(@"\d+(\.\d+)?", RegexOptions.Compiled);
#endif
    private static DataTable Table { get; } = new();

    public static long Parse(ReadOnlySpan<char> input)
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
