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
    private const string SessionURL = "https://kahoot.it/reserve/session/{0}/?{1}";
    /// <summary>
    /// Gets a reponse from Kahoot about the game code
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    /// <exception cref="GameNotFoundException"></exception>
    internal static async Task<(CreateSessionResponse? response, string? header)> CreateSessionResponseAsync(int gameId, HttpClient client)
    {
        var data = await client.SendAsync(FormRequest(gameId));

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

    private static double GetSeconds()
    {
        TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1));
        return Math.Floor(span.TotalSeconds * 1000);
    }


    private static HttpRequestMessage FormRequest(int gameId)
    {
        HttpRequestMessage request = new();

        request.Method = HttpMethod.Get;

        request.RequestUri = new Uri(string.Format(SessionURL, gameId, GetSeconds()));

        request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
        request.Headers.Add("Referer", "https://kahoot.it");
        request.Headers.Add("Accept", "*/*");

        return request;
    }
}
