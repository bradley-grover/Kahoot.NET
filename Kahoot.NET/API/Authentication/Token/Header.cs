namespace Kahoot.NET.API.Authentication.Token;

internal static class Header
{
    /// <summary>
    /// Creates the Header section internally to be combined with the challenge function token
    /// </summary>
    /// <param name="header">Session header</param>
    /// <returns>Portion for websocket key to be combined</returns>
    public static ReadOnlySpan<char> Create(ReadOnlySpan<char> header)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(header.ToString()).AsSpan());
    }
}
