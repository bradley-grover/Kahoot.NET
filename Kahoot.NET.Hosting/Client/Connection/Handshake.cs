using Kahoot.NET.API;
using Kahoot.NET.API.Requests.Handshake;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Hosting.Client;

public partial class KahootHost
{
    internal async Task SendHandshakeAsync(int code, string key)
    {
        _code = code;

        Uri uri = new(string.Format(ConnectionInfo.HostWebsocketUrl, code, key));

        Logger?.LogDebug("{url}", uri);

        Logger?.LogDebug("Connecting to socket ...");

        await Socket.ConnectAsync(uri, CancellationToken.None);

        await SendAsync(new ClientHandshake()
        {
            Id = State.id.ToString(),
            Ext = new()
            {
                Acknowledged = true,
                Time = new()
                {
                    L = 0,
                    O = 0,
                }
            }
        }, ClientHandshakeContext.Default.ClientHandshake);
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
                    O = State.o
                }
            },
            Id = Interlocked.Read(ref State.id).ToString()
        };
    }
}
