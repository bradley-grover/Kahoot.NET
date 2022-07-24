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
            message = Serializer.Serialize(data, out json);
        }
        else
        {
            message = Serializer.Serialize(data, typeInfo, out json);
        }

        Logger?.LogDebug("[SEND]: {message}", json.ToString());

        return message;
    }
}
