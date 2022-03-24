namespace Kahoot.NET.Internal.Token.Processing;

/// <summary>
/// Decoded the header token in the webrequest to be used to combine the tokens
/// </summary>
internal static class Header
{
    /// <summary>
    /// Creates the header portion of the token
    /// </summary>
    /// <param name="header">The header from the request</param>
    /// <returns>Converts into a UTF-8 encoded string</returns>
    public static ReadOnlySpan<char> CreateHeaderToken(ReadOnlySpan<char> header)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(header.ToString()));
    }
}
