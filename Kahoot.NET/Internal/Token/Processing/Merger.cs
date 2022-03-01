namespace Kahoot.NET.Internal.Token.Processing;

internal static class Merger
{
    public static string Create(ReadOnlySpan<char> header, ReadOnlySpan<char> challenge)
    {
        Span<char> merged = new char[header.Length];

        for (int i = 0; i < header.Length; i++)
        {
            var character = (int)header[i];
            var mod = (int)challenge[i % challenge.Length];
            var decoded = character ^ mod;
            merged[i] = Convert.ToChar(decoded);
        }
        return new string(merged);
    }
}
