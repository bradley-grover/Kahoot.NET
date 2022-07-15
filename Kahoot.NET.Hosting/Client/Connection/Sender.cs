using Kahoot.NET.Client;

namespace Kahoot.NET.Hosting.Client;

public partial class KahootHost
{
    internal async Task SendAsync<T>(T data, JsonTypeInfo<T>? typeInfo = null)
        where T : class
    {
        await Socket.SendAsync(LogInfo(data, typeInfo), WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, CancellationToken.None);
    }

    internal Memory<byte> LogInfo<T>(T data, JsonTypeInfo<T>? typeInfo)
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
}
