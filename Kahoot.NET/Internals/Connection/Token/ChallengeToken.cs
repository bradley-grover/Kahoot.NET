namespace Kahoot.NET.Internals.Connection.Token;

/// <summary>
/// Provides methods to compute the first part of the token
/// </summary>
internal static class ChallengeToken
{
    #region Parsers
    private static readonly IParser<char> Offset = new OffsetParser();
    private static readonly IParser<char> Parameter = new SpecialParameterParser();
    private static readonly IValueParser<long> ComputeOffset = new LargeOffsetParser();
    #endregion


    /// <summary>
    /// Gets the challenge part of token
    /// </summary>
    /// <param name="token"></param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> to be used for <see cref="Token"/></returns>
    public static ReadOnlySpan<char> CreateToken(ReadOnlySpan<char> token)
    {
        var param = Parameter.Parse(token);

        var offset = GetOffset(Offset.Parse(token.RemoveWhitespace()));

        return Decode(param, offset);
    }


    internal static char Repl(char character, int position, long offset)
    {
        int combined = character * position;

        long result = ((combined + offset)%77)+48;

        return Convert.ToChar(result);
    }

    /// <summary>
    /// Computes the offset string to a integer
    /// </summary>
    /// <param name="offsetString">The expression</param>
    /// <returns>Value of the offset</returns>
    internal static long GetOffset(ReadOnlySpan<char> offsetString)
    {
        return ComputeOffset.Parse(offsetString);
    }
    /// <summary>
    /// Decodes the key from the challenge string to get the first part of the token
    /// </summary>
    /// <param name="value">The key</param>
    /// <param name="offset">The offset to calculate the part of the token</param>
    /// <returns></returns>

    public static ReadOnlySpan<char> Decode(ReadOnlySpan<char> value, long offset)
    {
        StringBuilder builder = new();

        for (int i = 0; i < value.Length; i++)
        {
            builder.Append(Repl(value[i], i, offset));
        }

        return builder.ToString();
    }
    #region HideObsolete
    [Obsolete("Solution memory ineffiency and speed", false)]
    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }
    #endregion
}
