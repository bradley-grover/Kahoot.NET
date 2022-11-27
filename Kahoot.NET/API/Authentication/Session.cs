using Kahoot.NET.API.Authentication.Token;

namespace Kahoot.NET.API.Authentication;

/// <summary>
/// Static class to create a session
/// </summary>
internal static class Session
{
    /// <summary>
    /// Creates the session used to connect to the game
    /// </summary>
    /// <param name="client">Http client to be used for sending the request</param>
    /// <param name="gameId">The game id to use</param>
    /// <returns>Session response which may have failed</returns>
    internal static async Task<SessionResponse> CreateAsync(HttpClient client, int gameId)
    {
        HttpResponseMessage response;

        try
        {
            response = await client.SendGameAsync(gameId);
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine(ex.ToString());
            return SessionResponse.Failed;
        }

        if (!response.IsSuccessStatusCode || 
            !response.Headers.TryGetValues(Connection.SessionHeader, out var headers))
        {
            return SessionResponse.Failed;
        }

        var header = headers.FirstOrDefault();

        if (header is null)
        {
            return SessionResponse.Failed;
        }

        var session = JsonSerializer.Deserialize(
            await response.Content.ReadAsStringAsync(), SessionContext.Default.SessionResponse);

        if (session is null)
        {
            return SessionResponse.Failed;
        }

        session.WebSocketKey = Key.Create(header, session.Challenge);
        session.Success = true;

        return session;
    }
}
