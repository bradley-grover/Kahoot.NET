namespace Kahoot.NET.Internal.Token.Processing;

internal static class Header
{
    public static ReadOnlySpan<char> CreateHeaderToken(ReadOnlySpan<char> header)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(header.ToString()));
    }
}
