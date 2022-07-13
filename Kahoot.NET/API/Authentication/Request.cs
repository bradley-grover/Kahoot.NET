namespace Kahoot.NET.API.Authentication;

/// <summary>
/// Handles sending the authentication request used when getting the websocket key to connect to the game
/// </summary>
internal static class Request
{
    /// <summary>
    /// Sends a HTTP GET request to Kahoot to retrieve game information
    /// </summary>
    /// <param name="client"><see cref="HttpClient"/> to be used to send the request</param>
    /// <param name="gameId">The id of the game</param>
    /// <returns><see cref="HttpResponseMessage"/> from sending the request</returns>
    internal static Task<HttpResponseMessage> SendGameAsync(this HttpClient client, int gameId)
    {
        ArgumentNullException.ThrowIfNull(client);

        return client.SendAsync(New(gameId));
    }

    /// <summary>
    /// Creates the request to send to Kahoot
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    internal static HttpRequestMessage New(int gameId)
    {
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(string.Format(Connection.SessionUrl, gameId, CalculateSeconds()))
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
