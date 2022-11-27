namespace Kahoot.NET.API.Authentication.Token;

/// <summary>
/// Static class to retrieve header part of the websocket key to combine
/// </summary>
internal static class Header
{
    /// <summary>
    /// Creates the Header section internally to be combined with the challenge function token
    /// </summary>
    /// <param name="header">Session header</param>
    /// <returns>Portion for websocket key to be combined</returns>
    public static ReadOnlySpan<char> Create(ReadOnlySpan<char> header)
    {
        Span<byte> bytes = stackalloc byte[1024];

        _ = Convert.TryFromBase64Chars(header, bytes, out int written);

        return Encoding.UTF8.GetString(bytes[..written]);
    }
}
