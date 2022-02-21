using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kahoot.NET.Client.InternalConnectionState;
using Kahoot.NET.Internals.Messages.Handshake;

namespace Kahoot.NET.Client;

/// <inheritdoc></inheritdoc>
public partial class KahootClient
{
    private readonly CurrentSession _sessionState = new();

    internal async Task SendAsync<TData>(TData data, CancellationToken cancellationToken = default) where TData : class
    {
        if (WebSocket is null)
        {
            throw new InvalidOperationException("Connection must be created first before sending data");
        }

        await WebSocket.SendAsync(LogSend(data), WebSocketMessageType.Text, true, cancellationToken);
    }
    private ArraySegment<byte> LogSend<T>(T data)
    {
        ArraySegment<byte> response = ClientSerializer.Serialize(data, out var json);
        Logger?.LogDebug("Sending: {message}", json.ToString());
        return response;
    }
    internal async Task SendFirstMessageAsync(CancellationToken cancellationToken = default)
    {
        await SendAsync(new FirstHandshake() { Ext = new() { Acknowledged = true } }, cancellationToken);
    }

    internal async Task SendSecondHandshakeAsync(CancellationToken cancellationToken = default)
    {
        await SendAsync(CreateSecondHandshake(), cancellationToken);
    }
    internal SecondHandshake CreateSecondHandshake()
    {
        if (_sessionState.LiveTimesyncData is null)
        {
            throw new InvalidOperationException("Lag and network information has not been parsed");
        }

        var (L, O) = _sessionState.LiveTimesyncData.Value;

        return new SecondHandshake()
        {
            Advice = new() { TimeOut = 0 },

            ClientId = ClientToken,

            Ext = new()
            {
                Timesync = new() { L = L, O = O }
            }
        };
    }
}
