using System.Buffers;
using Kahoot.NET.API;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ReceiveAsync()
    {
        if (Socket.State is not WebSocketState.Open)
        {
            await ClientError.InvokeEventAsync(this, new(new InvalidOperationException("Connection is not open for operation")));
        }

        while (Socket.State == WebSocketState.Open)
        {
            byte[] array = ArrayPool<byte>.Shared.Rent(StateObject.BufferSize);

            try
            {
                Memory<byte> buffer = array;

                var result = await Socket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    return;
                }

                await ProcessDataAsync(buffer[..result.Count]);
            }
            catch (Exception ex)
            {
                Logger?.LogError("{exception}", ex.Message);
                throw;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(array);
            }
        }
    }
}
