namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task SendAsync<TData>(TData data, CancellationToken cancellationToken = default) where TData : class
    {
        if (Socket is null)
        {
            throw new InvalidOperationException("Socket is not connected");
        }

        await Socket.SendAsync(LogSend(data), WebSocketMessageType.Text,  WebSocketMessageFlags.EndOfMessage, cancellationToken);
    }
    private Memory<byte> LogSend<T>(T data)
    {
        Memory<byte> message = InternalSerializer.Serialize(data, out var json);

        Logger?.LogDebug("Sending: {message}", json.ToString());

        return message;
    }
}
