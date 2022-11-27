using Kahoot.NET.Client;

namespace Kahoot.NET.Hosting.Client;

public partial class KahootHost
{
    internal async Task SendAsync<T>(T data, JsonTypeInfo<T>? typeInfo = null)
        where T : class
    {
        await Socket.SendAsync(LogSend(data, typeInfo), WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, CancellationToken.None);
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
        Memory<byte> memory;

        if (typeInfo is null)
        {
            memory = JsonSerializer.SerializeToUtf8Bytes(data, SerializerOptions);
        }
        else
        {
            memory = JsonSerializer.SerializeToUtf8Bytes(data, typeInfo);
        }

        return memory;
    }
#endif
    internal static JsonSerializerOptions SerializerOptions { get; } = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
