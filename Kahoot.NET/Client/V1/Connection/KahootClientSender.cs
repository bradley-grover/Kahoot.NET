using System.Text.Json.Serialization.Metadata;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task SendAsync<TData>(TData data, JsonTypeInfo<TData>? typeInfo = null, CancellationToken cancellationToken = default) where TData : class
    {
        if (Socket is null)
        {
            throw new InvalidOperationException("Socket is not connected");
        }

        await Socket.SendAsync(LogSend(data, typeInfo), WebSocketMessageType.Text,  WebSocketMessageFlags.EndOfMessage, cancellationToken);
    }
    private Memory<byte> LogSend<T>(T data, JsonTypeInfo<T>? typeInfo = null)
    {
        ReadOnlySpan<char> json;
        Memory<byte> message;

        if (typeInfo is null)
        {
            message = InternalSerializer.Serialize(data, out json);
        }
        else
        {
            message = InternalSerializer.Serialize(data, typeInfo, out json);
        }

        Logger?.LogDebug("Sending: {message}", json.ToString());

        return message;
    }

    private async Task SendKeepAliveAsync()
    {
        await SendAsync(new KeepAlive()
        {
            Channel = LiveMessageChannels.Connection,
            ClientId = _sessionObject.clientId,
            ConnectionType = InternalConsts.ConnectionType,
            Id = Interlocked.Increment(ref _sessionObject.id).ToString(),
            Ext = new()
            {
                Acknowledged = Interlocked.Read(ref _sessionObject.ack),
                Timesync = new()
                {
                    L = _sessionObject.l,
                    O = _sessionObject.o,
                }
            }
        });
        Interlocked.Increment(ref _sessionObject.ack);
    }
}
