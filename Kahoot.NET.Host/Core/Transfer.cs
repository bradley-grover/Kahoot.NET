using System.Buffers;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;


#if DEBUG // see comment in DataProcessing.cs to see what this means
//#define VIEWTEXT
#endif

#if !VIEWTEXT
#pragma warning disable CA1822 // Mark members as static
#endif

namespace Kahoot.NET.Host;

public partial class KahootHost
{
    internal async ValueTask SendAsync<TData>(TData data, JsonTypeInfo<TData>? typeInfo = null, CancellationToken cancellationToken = default)
    {
        Debug.Assert(_ws != null);

        await _sendLock.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            await _ws.SendAsync(GetData(data, typeInfo), WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, cancellationToken);
        }
        finally
        {
            _sendLock.Release();
        }
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
        if (_ws.State == WebSocketState.Open)
        {
            return;
        }

        while (_ws.State == WebSocketState.Open)
        {
            byte[] bytes = ArrayPool<byte>.Shared.Rent(StateObject.BufferSize);

            Memory<byte> buffer = bytes;

            try
            {
                var result = await _ws.ReceiveAsync(buffer, default);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, default);
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("{err}", ex);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(bytes);
            }
        }
    }
}
