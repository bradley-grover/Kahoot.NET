using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using Kahoot.NET.Internals.Messages;
using Kahoot.NET.Internals.Messages.Handshake;
using System.Diagnostics;

namespace Kahoot.NET.Client;

/*
 * This partial class code file is used for the WebSocket connection to Kahoot
 */

/// <inheritdoc></inheritdoc>/>
public partial class KahootClient : IKahootClient
{
    private ClientWebSocket? WebSocket { get; set; }
    private const string WebsocketUrl = "wss://kahoot.it/cometd/{0}/{1}";
    private bool disposedValue;

    internal async Task CreateHandshakeAsync(CancellationToken cancellationToken = default)
    {
        if (GameId is null)
        {
            throw new NoGameIdException();
        }

        Logger?.LogInformation("Attemping to create token");

        (var token, var response) = await Token.CreateTokenSessionAsync(GameId.Value, Client);

        Socket = new();

        Logger?.LogInformation("Attemping to connect to websocket");

        await WebSocket.ConnectAsync(
            new Uri(string.Format(WebsocketUrl, GameId.Value, token)),
            cancellationToken);
    }

    public async Task ExecuteAndWaitForDataAsync(CancellationToken cancellationToken = default)
    {
        Memory<byte> buffer = new byte[512];

        if (WebSocket is null)
        {
            throw new InvalidOperationException();
        }

        while (WebSocket.State == WebSocketState.Open)
        {
            var result = await WebSocket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }

            await ProcessAsync(buffer, result.Count);
        }

    }

    /// <summary>
    /// Processes websocket data to events
    /// </summary>
    /// <param name="data"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private async Task ProcessAsync(Memory<byte> data, int count)
    {
        if (!_sessionState.FirstMessageReceivedBack)
        {
            var response = JsonSerializer.Deserialize<FirstServerResponse>(Encoding.UTF8.GetString(data.ToArray(), 0, count));
            if (response is null)
            {
                throw new InvalidOperationException();
            }

            ParseFirstResponse(response);
            return;
        }
    }

        if (OnJoined is not null)
        {
            await OnJoined.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Disposes the Client
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                WebSocket?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            disposedValue = true;
        }
    }
    /// <inheritdoc></inheritdoc>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
