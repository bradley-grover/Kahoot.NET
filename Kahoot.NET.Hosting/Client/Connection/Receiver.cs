using System.Buffers;

namespace Kahoot.NET.Hosting.Client;

public partial class KahootHost
{
    internal async Task ReceiveAsync()
    {
        while (Socket.State == WebSocketState.Open)
        {
            byte[] bytes = ArrayPool<byte>.Shared.Rent(StateObject.BufferSize);

            try
            {
                Memory<byte> buffer = bytes;

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
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(bytes);
            }
        }
    }
}
