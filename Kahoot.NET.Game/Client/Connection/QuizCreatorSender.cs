using Kahoot.NET.Client;
using System.Text.Json.Serialization.Metadata;

namespace Kahoot.NET.Game.Client;

public partial class QuizCreator
{
    internal async Task SendAsync<TData>(TData data, JsonTypeInfo<TData>? typeInfo = null, CancellationToken cancellationToken = default) where TData : class
    {
        if (Socket is null)
        {
            throw new InvalidOperationException("Socket is not connected");
        }

        await Socket.SendAsync(LogSend(data, typeInfo), WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, cancellationToken);
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
}
