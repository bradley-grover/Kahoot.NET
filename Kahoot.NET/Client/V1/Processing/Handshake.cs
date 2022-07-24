using Kahoot.NET.API.Authentication;
using Kahoot.NET.API.Requests.Handshake;
using Kahoot.NET.API.Requests.Json;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task<bool> SendHandshakeAsync(CancellationToken cancellationToken = default)
    {
        var session = await Session.CreateAsync(Client, GameId);

        if (!session.Success)
        {
            return false;
        }

        usingNamerator = session.Namerator;

        Uri uri = new(string.Format(Connection.WebsocketUrl, GameId, session.WebSocketKey));

        Logger?.LogDebug("{url}", uri);

        Logger?.LogDebug("Connecting to socket...");

        Socket.Options.KeepAliveInterval = TimeSpan.Zero;

        await Socket.ConnectAsync(uri, cancellationToken);

        await SendAsync(new ClientHandshake()
        {
            Id = Interlocked.Read(ref State.id).ToString(),
            Ext = new() { Acknowledged = true, Time = new() { L = 0, O = 0 } }
        }, ClientHandshakeContext.Default.ClientHandshake, cancellationToken);

        return true;
    }

    internal SecondClientHandshake CreateHS2()
    {
        return new()
        {
            ClientId = State.clientId,
            Ext = new()
            {
                Acknowledged = Interlocked.Read(ref State.ack),
                Time = new()
                {
                    L = State.l,
                    O = State.o,
                }
            },
            Id = Interlocked.Read(ref State.id).ToString(),
        };
    }

    internal LastHs CreateLastHS()
    {
        return new()
        {
            ClientId = State.clientId,
            Id = Interlocked.Read(ref State.id).ToString(),
            Ext = new()
            {
                Acknowledged = Interlocked.Read(ref State.ack),
                Time = new()
                {
                    L = State.l,
                    O = State.o
                }
            }
        };
    }
}
