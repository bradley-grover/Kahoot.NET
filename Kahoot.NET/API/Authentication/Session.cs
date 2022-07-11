using Kahoot.NET.API.Authentication.Json;
using Kahoot.NET.API.Authentication.Token;

namespace Kahoot.NET.API.Authentication;

internal static class Session
{
    internal static async Task<SessionResponse> CreateAsync(HttpClient client, int gameId)
    {
        HttpResponseMessage response = await client.SendGameAsync(gameId);

        if (!response.IsSuccessStatusCode || 
            !response.Headers.TryGetValues(Connection.SessionHeader, out var headers))
        {
            return Failed();
        }

        var header = headers.FirstOrDefault();

        if (header is null)
        {
            return Failed();
        }

        var session = JsonSerializer.Deserialize(
            await response.Content.ReadAsStringAsync(), SessionContext.Default.SessionResponse);

        if (session is null)
        {
            return Failed();
        }

        session.WebSocketKey = Key.Create(header, session.Challenge);
        session.Success = true;

        return session;
    }

    private static SessionResponse Failed()
    {
        return new() { Success = false };
    }
}
