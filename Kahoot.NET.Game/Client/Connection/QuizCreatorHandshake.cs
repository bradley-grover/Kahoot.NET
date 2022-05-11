using Kahoot.NET.Game.Internal.Data.Messages;
using Kahoot.NET.Internal;
using Kahoot.NET.Internal.Data.Messages;
using Kahoot.NET.Internal.Data.Messages.Handshake;
using Kahoot.NET.Internal.Data.SourceGenerators.Messages;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Game.Client;

public partial class QuizCreator
{
    private int _gameCode;
    internal async Task CreateHandshakeAsync(int gameCode, string websocketKey)
    {
        _gameCode = gameCode;

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

    internal SecondLiveClientHandshake CreateSecondHandshake()
    {
        Interlocked.Increment(ref _sessionObject.id);
        _sessionObject.ack = 0;
        _sessionObject.o = 2236;
        _sessionObject.l = 68;

        return new()
        {
            Advice = new()
            {
                Timeout = 0,
            },
            Ext = new()
            {
                Acknowledged = _sessionObject.ack,
                Timesync = new()
                {
                    O = _sessionObject.o,
                    L = _sessionObject.l
                }
            },
            Channel = LiveMessageChannels.Connection,
            ClientId = _sessionObject.clientId,
            ConnectionType = InternalConsts.ConnectionType,
            Id = _sessionObject.id.ToString()
        };
    }

    internal StartGameMessage CreateStartGameMessage()
    {
        return new StartGameMessage()
        {
            Channel = LiveMessageChannels.Player,
            Id = _sessionObject.id.ToString(),
            ClientId = _sessionObject.clientId,
            Data = new()
            {
                GameId = gameCode.ToString(),
                Type = "started",
                Host = "play.kahoot.it"
            }
        };
    }
    internal KeepAlive CreateKeepAlive()
    {
        return new KeepAlive()
        {
            Channel = LiveMessageChannels.Connection,
            ClientId = _sessionObject.clientId,
            ConnectionType = InternalConsts.ConnectionType,
            Id = _sessionObject.id.ToString(),
            Ext = new()
            {
                Acknowledged = _sessionObject.ack,
                Timesync = new()
                {
                    L = _sessionObject.l,
                    O = _sessionObject.o
                }
            }
        };
    }
}
