namespace Kahoot.NET.API.Authentication;

/// <summary>
/// Static class used to retrieve game meta data so that we can initialize a websocket connection to Kahoot!
/// </summary>
public static class Session
{
    /// <summary>
    /// Sends an HTTP request to the specified Kahoot! code url, and receives a response to use further
    /// </summary>
    /// <param name="client">An http client to send requests to the Kahoot! url</param>
    /// <param name="gameId">The code of the game</param>
    /// <returns>A <see cref="SessionResponse"/> containing information used to join the game</returns>
    public static async Task<SessionResponse> CreateAsync(HttpClient client, uint gameId)
    {
        HttpResponseMessage response;

        try
        {
            response = await Request.QueryGameAsync(client, gameId);
        }
        catch (HttpRequestException)
        {
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
    /// Gets a configured WebSocket that can be used to communicate with the Kahoot live game
    /// </summary>
    /// <param name="receiveBufferSize">The amount of bytes that can be received at a time</param>
    /// <param name="sendBufferSize">The amount of bytes that can be sent at a time</param>
    /// <returns>A configure <see cref="ClientWebSocket"/></returns>
    /// <exception cref="ArgumentException">If the buffers are below 1024, the min amount it will throw an argument exception</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ClientWebSocket GetConfiguredWebSocket(int receiveBufferSize, int sendBufferSize)
    {
        if (receiveBufferSize < 1024 || sendBufferSize < 1024)
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
