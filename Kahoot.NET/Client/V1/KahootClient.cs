namespace Kahoot.NET.Client;

/// <summary>
/// First version of <see cref="IKahootClient"/> to be used to connect and interact with Kahoot games
/// </summary>
public partial class KahootClient : IKahootClient
{
    /// <inheritdoc></inheritdoc>
    public async Task<bool> JoinAsync(int code, string? username = null, CancellationToken cancellationToken = default)
    {
        if (_inGame)
        {
            throw new InvalidOperationException("The client is already in a game");
        }

        Username = username;
        GameId = code;

        Logger?.LogDebug("Creating handshake");

        var result = await SendHandshakeAsync(cancellationToken);

        if (!result)
        {
            return false;
        }

        Logger?.LogDebug("Data thread spawning");

        var backgroundProcesser = new Thread(async () => await ReceiveAsync())
        {
            IsBackground = true
        };

        backgroundProcesser.Start();

        _inGame = true;

        return true;
    }

    /// <inheritdoc></inheritdoc>
    public async Task LeaveAsync(CancellationToken cancellationToken = default)
    {
        if (Socket is null || Socket.State != WebSocketState.Open)
        {
            throw new InvalidOperationException("Can't perform this operation when the client isn't connected");
        }

        await SendLeaveMessageAsync();

        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);

        await Left.InvokeEventAsync(this, new(ReasonForLeaving.UserRequested));

        _inGame = false;
    }
}
