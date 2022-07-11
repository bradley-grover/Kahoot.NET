using Kahoot.NET.Internal.Data.Shared.Ext;
using Kahoot.NET.Internal.Data.Messages.Handshake;
using Kahoot.NET.Internal.Data.Responses.Handshake;
using Kahoot.NET.API.Authentication;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task<bool> CreateHandshakeAsync(CancellationToken cancellationToken = default)
    {
        if (GameId is null)
        {
            throw new NoGameIdException();
        }

        var session = await Session.CreateAsync(Client, GameId.Value);

        if (!session.Success)
        {
            return false;
        }

        usingNamerator = session.Namerator;

        Uri uri = new(string.Format(WebsocketUrl, GameId.Value, session.WebSocketKey));

        Logger?.LogDebug("{url}", uri);

        Logger?.LogDebug("Connecting to socket...");

        await Socket.ConnectAsync(uri, cancellationToken);

        await SendAsync(CreateFirstHandshakeObject(), LiveClientHandshakeContext.Default.LiveClientHandshake, cancellationToken);

        return true;
    }



    internal LiveClientHandshake CreateFirstHandshakeObject()
    {
        return new LiveClientHandshake()
        {
            Advice = new() { Interval = 0, Timeout = 60_000 },
            MinimumVersion = InternalConsts.MinVersion,
            Version = InternalConsts.Version,
            SupportedConnectionTypes = InternalConsts.SupportedConnectionTypes,
            Channel = LiveMessageChannels.Handshake,
            Id = Interlocked.Read(ref State.id).ToString(),
            Ext = new() { Acknowledged = true, Timesync = new() { L = 0, O = 0 } }
        };
    }

    internal FinalHandshake CreateFinalHandshake()
    {
        return new FinalHandshake()
        {
            Channel = LiveMessageChannels.Connection,
            ConnectionType = InternalConsts.ConnectionType,
            ClientId = State.clientId,
            Id = Interlocked.Read(ref State.id).ToString(),
            Ext = new()
            {
                Acknowledged = Interlocked.Read(ref State.ack),
                Timesync = new()
                {
                    L = State.l,
                    O = State.o
                }
            }
        };
    }

    internal SecondLiveClientHandshake CreateSecondHandshakeObject(LiveClientHandshakeResponse response)
    {
        Logger?.LogDebug("{}", JsonSerializer.Serialize(response));
        //(var l, var o) = GetLagAndOffset(response.Ext);
        State.l = 68;
        State.o = 2999;

        return new()
        {
            Advice = new() { Timeout = 0 },
            ConnectionType = InternalConsts.ConnectionType,
            ClientId = State.clientId,
            Ext = new()
            {
                Acknowledged = Interlocked.Read(ref State.ack),
                Timesync = new()
                {
                    L = State.l,
                    O = State.o,
                }
            },
            Id = Interlocked.Read(ref State.id).ToString(),
            Channel = LiveMessageChannels.Connection,
        };
    }

    internal static (long L, long O) GetLagAndOffset(ExtWithExtendedTimesyncData data)
    {
        long l = (DateTime.UtcNow.Millisecond - data.Timesync.CurrentTime) / 2;
        long o = (data.Timesync.Ts - data.Timesync.CurrentTime - 1);

        return (l, o);
    }
}
