namespace Kahoot.NET.API.Authentication;

/// <summary>
/// Static class to create a session
/// </summary>
public static class Session
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
            response = await Request.QueryGameAsync(client, gameId);
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine(ex.ToString());
            return SessionResponse.Failed;
        }

        if (!response.IsSuccessStatusCode || 
            !response.Headers.TryGetValues(ConnectionInfo.SessionHeader, out var headers))
        {
            return SessionResponse.Failed;
        }

        var header = headers.FirstOrDefault();

        if (header is null)
        {
            return SessionResponse.Failed;
        }

        var session = await JsonSerializer.DeserializeAsync(
            await response.Content.ReadAsStreamAsync(), SessionContext.Default.SessionResponse);

        if (session is null)
        {
            return SessionResponse.Failed;
        }

        session.WebSocketKey = WebSocketKey.Create(header, session.Challenge!);
        session.Success = true;

        return session;
    }

    /// <summary>
    /// Configures a <see cref="ClientWebSocket"/> that uses the optimal settings for Kahoot
    /// </summary>
    /// <remarks>
    /// Recommended value is the minimum value for both buffer size is 1024
    /// </remarks>
    /// <param name="receiveBufferSize"></param>
    /// <param name="sendBufferSize"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ClientWebSocket GetConfiguredWebSocket(int receiveBufferSize, int sendBufferSize)
    {
        if (receiveBufferSize < 1024 | sendBufferSize < 1024)
        {
            throw new ArgumentException("The receive buffer should be at least 1024 to handle messages", 
                nameof(receiveBufferSize));
        }

        ClientWebSocket socket = new();

        socket.Options.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");

        socket.Options.SetBuffer(receiveBufferSize, sendBufferSize);

        socket.Options.KeepAliveInterval = TimeSpan.Zero;

        return socket;
    }
}
