namespace Kahoot.NET.API.Authentication;

internal static class Header
{
    public static int GetHeader(ReadOnlySpan<char> sessionHeader, Span<char> chars)
    {
        Span<byte> bytes = stackalloc byte[sessionHeader.Length * 4 / 3];

        _ = Convert.TryFromBase64Chars(sessionHeader, bytes, out int written);

        return Encoding.UTF8.GetChars(bytes[..written], chars);
    }
}
