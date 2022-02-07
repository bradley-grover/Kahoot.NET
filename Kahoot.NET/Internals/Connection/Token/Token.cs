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
    public static async Task<string> CreateTokenAsync(int id, HttpClient client)
    {
        var (response, header) = await ConnectionHelper.CreateSessionResponseAsync(id, client);

        if (response is null)
        {
            throw new GameNotFoundException();
        }


        return CombineTokens(HeaderToken.CreateHeaderToken(header), ChallengeToken.CreateToken(response.Challenge));
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
}
