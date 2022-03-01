using Kahoot.NET.Internal.Data.Responses;

namespace Kahoot.NET.Internal.Token.RequestFormer;

/// <summary>
/// Forms the requests to find the token used for the websocket connection
/// </summary>
internal static class RequestFormer
{
    private const string SessionHeader = "x-kahoot-session-token";
    private const string SessionUrl = "https://kahoot.it/reserve/session/{0}/?{1}";

    /// <summary>
    /// Creates a session on kahoot
    /// </summary>
    /// <param name="gameId">Game id, if this game doesn't exist it will throw an exception</param>
    /// <param name="client">HttpClient to be used to get a response from kahoot</param>
    /// <returns>Header data and a game response</returns>
    /// <exception cref="GameNotFoundException">Thrown if the id is invalid</exception>
    /// <exception cref="Exception">Thrown when the header is not found</exception>
    internal static async Task<(CreateSessionResponse?, string? header)> CreateSessionAsync(int gameId, HttpClient client)
    {
        var data = await client.SendAsync(NewRequest(gameId));

        if (!data.IsSuccessStatusCode)
        {
            throw new GameNotFoundException();
        }

        var content = await data.Content.ReadAsStringAsync();


        if (!data.Headers.TryGetValues(SessionHeader, out var headers))
        {
            throw new Exception();
        }

        return (JsonSerializer.Deserialize<CreateSessionResponse>(content), headers.FirstOrDefault());
    }

    /// <summary>
    /// Creates a request to send to kahoot
    /// </summary>
    /// <param name="gameId">The game id to join</param>
    /// <returns>A <see cref="HttpRequestMessage"/> used to ping kahoot and get the data back</returns>
    internal static HttpRequestMessage NewRequest(int gameId)
    {
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(string.Format(SessionUrl, gameId, CalculateSeconds()))
        };

        request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
        request.Headers.Add("Referer", "https://kahoot.it");
        request.Headers.Add("Accept", "*/*");

        return request;
    }
    /// <summary>
    /// Calculates the current seconds used in the request
    /// </summary>
    /// <returns>Current seconds since 1970</returns>
    internal static double CalculateSeconds()
    {
        return Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);
    }
}
