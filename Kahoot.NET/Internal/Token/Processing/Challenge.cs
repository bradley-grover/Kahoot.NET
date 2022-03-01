using Kahoot.NET.Internal.Token.Parsers;

namespace Kahoot.NET.Internal.Token.Processing;

internal static class Challenge
{
    private static IParser SpecialParam { get; } = new GetSpecialTokenParser();
    private static IParser OffsetSectionParser { get; } = new OffsetFinder();
    private static IValueParser<long> OffsetCalculator { get; } = new OffsetCalculator();


    internal static ReadOnlySpan<char> CreateToken(ReadOnlySpan<char> token)
    {
        var parameter = SpecialParam.Parse(token);

        var offset = OffsetCalculator.Parse(OffsetSectionParser.Parse(token.RemoveWhitespace()));

        return Decode(parameter, offset);
    }

    internal static ReadOnlySpan<char> Decode(ReadOnlySpan<char> value, long offset)
    {
        Span<char> decoded = new char[value.Length];

        for (int i = 0; i < value.Length; i++)
        {
            decoded[i] = Repl(value[i], i, offset);
        }

        return decoded;
    }

    internal static char Repl(char character, int position, long offset)
    {
        int combined = character * position;

        long result = ((combined + offset) % 77) + 48;

        return Convert.ToChar(result);
    }
}
