namespace Kahoot.NET.API.Authentication;

internal static class Request
{
    internal static Task<HttpResponseMessage> SendGameAsync(this HttpClient client, int gameId)
    {
        ArgumentNullException.ThrowIfNull(client);

        return client.SendAsync(New(gameId));
    }
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
