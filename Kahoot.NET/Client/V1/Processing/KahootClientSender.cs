using System.Text.Json.Serialization.Metadata;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async ValueTask SendAsync<TData>(TData data, JsonTypeInfo<TData>? typeInfo = null, CancellationToken cancellationToken = default) where TData : class
    {
        await Socket.SendAsync(LogSend(data, typeInfo), WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, cancellationToken);
    }

#if DEBUG
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
#elif RELEASE
    private static Memory<byte> LogSend<T>(T data, JsonTypeInfo<T>? typeInfo = null)
    {
        if (typeInfo is null)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data, SerializerOptions);
        }
            
        return JsonSerializer.SerializeToUtf8Bytes(data, typeInfo);
    }
#endif
}
