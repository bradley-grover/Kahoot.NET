using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using Kahoot.NET.Internals.Messages;

namespace Kahoot.NET.Client;

/*
 * This partial class code file is used for the WebSocket connection to Kahoot
 */


public partial class KahootClient : IKahootClient
{
    private ClientWebSocket? Socket { get; set; }
    private const string WebsocketUrl = "wss://kahoot.it/cometd/{0}/{1}";
    private bool disposedValue;

    internal async Task CreateHandshakeAsync(CancellationToken cancellationToken = default)
    {
        if (GameId is null)
        {
            throw new NoGameIdException();
        }

        Logger?.LogInformation("Attemping to create token");

        string token = await Token.CreateTokenAsync(GameId.Value, Client);

        Socket = new();

        Logger?.LogInformation("Attemping to connect to websocket");

        await Socket.ConnectAsync(
            new Uri(string.Format(WebsocketUrl, GameId.Value, token)),
            cancellationToken);
        
    }
    /// <summary>
    /// Sends a message to the Kahoot websocket
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    internal async Task SendDataAsync(IMessageBase message, CancellationToken cancellationToken = default)
    {
        if (Socket is null)
        {
            return;
        }
        Logger?.LogInformation("Attemping to send data to socket .. ");
        await Socket.SendAsync(message.ToRawMessage(), WebSocketMessageType.Text, true, cancellationToken);
    }
    /// <summary>
    /// Receives data in a loop then invokes events
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    internal async Task ReceiveDataAsync(CancellationToken token)
    {
        if (Socket is null)
        {
            return;
        }

        Memory<byte> memory = new byte[1024];

        while (true)
        {
            var result = await Socket.ReceiveAsync(memory, token);

            await ProcessAsync(memory);
        }
    }
    /// <summary>
    /// Processes websocket data to events
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private async Task ProcessAsync(Memory<byte> data)
    {
        
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
                Socket?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
