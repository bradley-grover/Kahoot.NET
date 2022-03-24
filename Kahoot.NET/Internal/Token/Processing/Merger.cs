namespace Kahoot.NET.Internal.Token.Processing;

/// <summary>
/// Combines the header part of the token and the challenge part to form the final token
/// </summary>
internal static class Merger
{
    /// <summary>
    /// Creates the token
    /// </summary>
    /// <param name="header">The header portion of the token</param>
    /// <param name="challenge">The challenge portion of the token</param>
    /// <returns>The decoded token as an allocated string</returns>
    public static string Create(ReadOnlySpan<char> header, ReadOnlySpan<char> challenge)
    {
        // new span to allocate for the token
        Span<char> merged = new char[header.Length];

        for (int i = 0; i < header.Length; i++) // loop every character in the header
        {
            // black magic stuff
            var character = (int)header[i];
            var mod = (int)challenge[i % challenge.Length];
            var decoded = character ^ mod;
            merged[i] = Convert.ToChar(decoded);
        }
        return new string(merged); // allocate into a string
    }
}
