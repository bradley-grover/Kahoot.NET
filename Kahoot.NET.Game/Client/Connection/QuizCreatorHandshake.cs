using Kahoot.NET.Internal;
using Kahoot.NET.Internal.Data.Messages.Handshake;
using Kahoot.NET.Internal.Data.SourceGenerators.Messages;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Game.Client;

public partial class QuizCreator
{
    internal async Task CreateHandshakeAsync(int gameCode, string websocketKey)
    {
        Uri uri = new($"wss://play.kahoot.it/cometd/{gameCode}/{websocketKey}");

        Logger?.LogDebug("{url}", uri);

        Logger?.LogDebug("Connecting to socket...");

        await Socket.ConnectAsync(uri, CancellationToken.None);

        await SendAsync(new LiveClientHandshake()
        {
            Channel = LiveMessageChannels.Handshake,
            Id = _sessionObject.id.ToString(),
            Ext = new()
            {
                Acknowledged = true,
                Timesync = new()
                {
                    L = 0,
                    O = 0,
                },
            },
            Advice = new()
            {
                Interval = 0,
                Timeout = 60_000,
            },
            MinimumVersion = InternalConsts.MinVersion,
            SupportedConnectionTypes = InternalConsts.SupportedConnectionTypes,
            Version = InternalConsts.Version

        }, LiveClientHandshakeContext.Default.LiveClientHandshake);
    }
}
