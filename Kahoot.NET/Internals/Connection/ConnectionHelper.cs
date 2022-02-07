namespace Kahoot.NET.Internals.Connection;

/// <summary>
/// Contains static methods to help create a connection to the kahoot game
/// </summary>
internal static class ConnectionHelper
{
    /// <summary>
    /// The header for the session token
    /// </summary>
    private const string SessionHeader = "x-kahoot-session-token";
    /// <summary>
    /// Session
    /// </summary>
    private const string SessionURL = "https://kahoot.it/reserve/session/{0}";
    /// <summary>
    /// Gets a reponse from Kahoot about the game code
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    /// <exception cref="GameNotFoundException"></exception>
    internal static async Task<(CreateSessionResponse? response, string? header)> CreateSessionResponseAsync(int gameId, HttpClient client)
    {
        var data = await client.GetAsync(string.Format(SessionURL, gameId));

        if (!data.IsSuccessStatusCode)
        {
            throw new GameNotFoundException();
        }

        var content = await data.Content.ReadAsStringAsync();

        if (!data.Headers.TryGetValues(SessionHeader, out var headers))
        {
            throw new NoSessionHeaderFoundException(); 
        }

        return (JsonSerializer.Deserialize<CreateSessionResponse>(content), headers.FirstOrDefault());
    }
}
