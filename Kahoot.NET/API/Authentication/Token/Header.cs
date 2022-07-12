namespace Kahoot.NET.API.Authentication.Token;

internal static class Header
{
    public static ReadOnlySpan<char> Create(ReadOnlySpan<char> header)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(header.ToString()).AsSpan());
    }
}
