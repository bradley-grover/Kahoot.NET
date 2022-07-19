using System.Text.Json.Serialization.Metadata;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task SendAsync<TData>(TData data, JsonTypeInfo<TData>? typeInfo = null, CancellationToken cancellationToken = default) where TData : class
    {
        await Socket.SendAsync(LogSend(data, typeInfo), WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, cancellationToken);
    }
    private Memory<byte> LogSend<T>(T data, JsonTypeInfo<T>? typeInfo = null)
    {
        ReadOnlySpan<char> json;
        Memory<byte> message;

        if (typeInfo is null)
        {
            message = Serializer.Serialize(data, out json);
        }
        else
        {
            message = Serializer.Serialize(data, typeInfo, out json);
        }

        Logger?.LogDebug("[SEND]: {json}", json.ToString());

        return message;
    }
}
