using Kahoot.NET.Parsers;

namespace Kahoot.NET.API.Authentication.Token;

/// <summary>
/// Provides methods to decode the challenge string
/// </summary>
internal static class Challenge
{
    private static IParser Parameter { get; } = new TokenIdentifer();
    private static IParser OffsetSectionParser { get; } = new OffsetIdentifer();
    private static IValueParser<long> OffsetCalculator { get; } = new OffsetArithmetic();

    /// <summary>
    /// Creates the challenge token from the challenge string
    /// </summary>
    /// <param name="token">The undecoded token string</param>
    /// <returns>Decoded challenge</returns>
    internal static ReadOnlySpan<char> CreateToken(ReadOnlySpan<char> token)
    {
        // find the special parameter string used to decode
        var parameter = Parameter.Parse(token);

        // find and calculate the offset
        var offset = OffsetCalculator.Parse(OffsetSectionParser.Parse(token.RemoveWhiteSpace()));

        return Decode(parameter, offset);
    }

    /// <summary>
    /// Decodes the challenge string using an offset
    /// </summary>
    /// <param name="value">The challenge string</param>
    /// <param name="offset">The integer offset to be used</param>
    /// <returns>The challenge token</returns>
    internal static ReadOnlySpan<char> Decode(ReadOnlySpan<char> value, long offset)
    {
        // allocate new span for the new string
        Span<char> decoded = new char[value.Length];

        for (int i = 0; i < value.Length; i++)
        {
            // add decoded characters to span
            decoded[i] = Repl(value[i], i, offset);
        }

        return decoded;
    }

    internal static char Repl(char character, int position, long offset)
    {
        int combined = character * position;

        // use kahoots challenge method to decode it
        long result = ((combined + offset) % 77) + 48;

        // convert back to a character
        return Convert.ToChar(result);
    }
}
