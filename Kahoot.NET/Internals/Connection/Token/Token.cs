namespace Kahoot.NET.Internals.Connection.Token;

/// <summary>
/// Provides method to decode the challenge and retrieve the token required to connect to the kahoot websocket
/// </summary>
internal static class Token
{
    /// <summary>
    /// Creates token from Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    /// <exception cref="GameNotFoundException"></exception>
    public static async Task<(string, CreateSessionResponse)> CreateTokenSessionAsync(int id, HttpClient client)
    {
        var (response, header) = await ConnectionHelper.CreateSessionResponseAsync(id, client);

        if (response is null)
        {
            throw new GameNotFoundException();
        }

        return (CombineTokensTemp(HeaderToken.CreateHeaderToken(header), ChallengeToken.CreateToken(response.Challenge)), response);
    }

    /// <summary>
    /// Combines the two seperate tokens to form the final token
    /// </summary>
    /// <param name="header"></param>
    /// <param name="challenge"></param>
    /// <returns></returns>
    internal static string CombineTokens(ReadOnlySpan<char> header, ReadOnlySpan<char> challenge)
    {
        StringBuilder builder = new();
        for (int i = 0; i < header.Length; i++)
        {
            int charCodeAt = header[i];
            int mod = challenge[i % challenge.Length];

            int decodedChar = charCodeAt ^ mod;

            builder.Append(char.ConvertFromUtf32(decodedChar));
        }
        return builder.ToString();
    }

    internal static string CombineTokensTemp(ReadOnlySpan<char> header, ReadOnlySpan<char> challenge)
    {
        StringBuilder builder = new();

        for (int i = 0; i < header.Length; i++)
        {
            var character = (int)header[i];
            var mod = (int)challenge[i % challenge.Length];
            var decoded = character ^ mod;
            builder.Append(Convert.ToChar(decoded));
        }

        return builder.ToString();
    }

    internal static string CombineTokensFast(ReadOnlySpan<char> header, ReadOnlySpan<char> challenge)
    {
        Span<char> converted = new char[header.Length];

        for (int i = 0; i < header.Length; i++)
        {
            int charCodeAt = header[i];
            int mod = challenge[i % challenge.Length];
            int decodedChar = charCodeAt ^ mod;
            converted[i] = char.ConvertFromUtf32(decodedChar)[0];
        }

        return new string(converted);
    }
}
