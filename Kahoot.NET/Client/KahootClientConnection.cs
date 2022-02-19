using System.Buffers;

namespace Kahoot.NET.Client;

/*
 * This partial class code file is used for the WebSocket connection to Kahoot
 */

/// <inheritdoc></inheritdoc>/>
public partial class KahootClient : IKahootClient
{
    /// <summary>
    /// Potentially to be used for reconnection
    /// </summary>
    private string? ConnectionToken { get; set; }
    private string? ClientToken { get; set; }
    private ClientWebSocket? WebSocket { get; set; }
    internal const string WebsocketUrl = "wss://kahoot.it/cometd/{0}/{1}";
    private bool disposedValue;

    #region CreationAndExecution
    internal async Task CreateHandshakeAsync(CancellationToken cancellationToken = default)
    {
        if (GameId is null)
        {
            throw new NoGameIdException();
        }

        Logger?.LogInformation("Attemping to create token");

        (var token, var response) = await Token.CreateTokenSessionAsync(GameId.Value, Client);

        ParseResponse(response);

        WebSocket = new();

        ConnectionToken = token;

        Logger?.LogDebug("Token is formed: {token}", token);

        Logger?.LogInformation("Attemping to connect to websocket");

        await WebSocket.ConnectAsync(
            new Uri(string.Format(WebsocketUrl, GameId.Value, token)),
            cancellationToken);

        await SendFirstMessageAsync(cancellationToken);

        if (OnJoined is not null)
        {
            await OnJoined.Invoke(this, EventArgs.Empty);
        }
    }

    internal async Task ExecuteAndWaitForDataAsync()
    {

        if (WebSocket is null)
        {
            throw new InvalidOperationException();
        }
        // Buffer to store response 1024B or 1KB
        Memory<byte> buffer = new byte[1024];

        while (WebSocket.State == WebSocketState.Open)
        {
            var result = await WebSocket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                if (OnQuizDisconnect is not null)
                {
                    await OnQuizDisconnect.Invoke(this, EventArgs.Empty);
                }
            }

            await ProcessAsync(buffer);
        }
    }
    #endregion

    /// <summary>
    /// Processes websocket data to events
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private async Task ProcessAsync(Memory<byte> data)
    {
        string json = RemoveBrackets(Encoding.UTF8.GetString(data.Span));

        Logger?.LogDebug("Response received: {json}", json);

        if (!_sessionState.FirstMessageReceivedBack)
        {
            _sessionState.FirstMessageReceivedBack = true;
            var response = JsonSerializer.Deserialize<FirstServerResponse>(json);
            if (response is null)
            {
                throw new InvalidOperationException();
            }

            await ParseFirstResponse(response);
            return;
        }
    }

    private static string RemoveBrackets(ReadOnlySpan<char> span)
    {
        int start = 1;
        int end = span.LastIndexOf(']');

        return span.Slice(start, end - 1).ToString();
    }

    private async Task ParseFirstResponse(FirstServerResponse response)
    {
        if (!response.Successful)
        {
            throw new CouldNotEstablishConnectionException();
        }

        ClientToken = response.ClientId;

        if (OnJoined is not null)
        {
            await OnJoined.Invoke(this, EventArgs.Empty);
        }

        await SendSecondHandshakeAsync();
    }

    internal void ParseResponse(CreateSessionResponse response)
    {
    }


    private void ThrowIfNotConnected()
    {
        if (WebSocket == null || WebSocket.State == WebSocketState.Closed)
        {
            throw new NotConnectedException(); 
        }
    }
}
