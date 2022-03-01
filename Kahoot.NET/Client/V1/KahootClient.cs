namespace Kahoot.NET.Client;

/// <summary>
/// Ok
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
}
