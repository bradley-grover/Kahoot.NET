using Kahoot.NET.Internal.Data.Messages.Leaving;

namespace Kahoot.NET.Client;

/// <summary>
/// First version of <see cref="IKahootClient"/> to be used to connect and interact with Kahoot games
/// </summary>
public partial class KahootClient : IKahootClient
{
    /// <inheritdoc></inheritdoc>
    public async Task JoinAsync(int code, string? username = null, CancellationToken cancellationToken = default)
    {
        Username = username;
        GameId = code;

        Logger?.LogDebug("Creating handshake");

        await CreateHandshakeAsync(cancellationToken);

        Logger?.LogDebug("Spawning data loop");

        var backgroundProcesser = new Thread(async () => await ReceiveAsync())
        {
            IsBackground = true
        };

        backgroundProcesser.Start();
    }

    /// <inheritdoc></inheritdoc>
    public async Task LeaveAsync(CancellationToken cancellationToken = default)
    {
        if (Socket is null)
        {
            throw new InvalidOperationException("Can't perform this operation when the client isn't connected");
        }

        await SendAsync(new LiveLeaveMessage()
        {
            Id = _sessionObject.id.ToString(),
            Channel = LiveMessageChannels.Disconnection,
            ClientId = _sessionObject.clientId,
        }, LiveLeaveMessageContext.Default.LiveLeaveMessage, cancellationToken);


        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
    }
}
