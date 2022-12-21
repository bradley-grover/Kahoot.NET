#if DEBUG // see comment in DataProcessing.cs to see what this means
//#define VIEWTEXT
#endif

#if !VIEWTEXT
#pragma warning disable CA1822 // Mark members as static
#endif

using Kahoot.NET.API.Requests;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async ValueTask SendAsync<TData>(TData data, JsonTypeInfo<TData>? typeInfo = null, CancellationToken cancellationToken = default)
        where TData : class
    {
        await _socket.SendAsync(GetData(data, typeInfo), WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, cancellationToken);
    }


    internal byte[] GetData<T>(T data, JsonTypeInfo<T>? typeInfo = null)
    {
#if VIEWTEXT
        string json = typeInfo == null ? 
            JsonSerializer.Serialize(data, _serializerOptions) :
            JsonSerializer.Serialize(data, typeInfo);

        _logger?.LogDebug("[SEND]: {json}", json);
#endif

        if (typeInfo == null)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data, _serializerOptions);
        }

        return JsonSerializer.SerializeToUtf8Bytes(data, typeInfo);
    }

    internal async Task ReceiveAsync()
    {
        if (_socket.State != WebSocketState.Open)
        {
            // TODO: Add error handling
            return;
        }

        while (_socket.State == WebSocketState.Open)
        {
            byte[] bytes = ArrayPool<byte>.Shared.Rent(StateObject.BufferSize);

            Memory<byte> buffer = bytes;

            try
            {
                var result = await _socket.ReceiveAsync(buffer, default);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, default, default);

                    _userName = default;

                    return;
                }

                await ProcessAsync(buffer[..result.Count]);
            }
            catch (Exception exception)
            {
                _logger?.LogError("{exceptionMessage}", exception.Message);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(bytes);
            }
        }

        _userName = default;
    }

    internal async Task ReplyAsync()
    {
        Bump();
        await SendPacketAsync();
    }

    // common operations
    internal async Task SendPacketAsync() => await SendAsync(new Packet()
    {
        Id = Interlocked.Read(ref _stateObject.id).ToString(),
        Channel = Channels.Connect,
        Ext = _stateObject.ExtWithTimesync,
        ClientId = _stateObject.clientId,
        ConnectionType = ConnectionInfo.ConnectionType
    });
}
