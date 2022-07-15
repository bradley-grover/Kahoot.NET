using System.Buffers;

namespace Kahoot.NET.Hosting.Client;

/*
 * The buffer in this code uses ArrayPool instead of new byte[] like in kahoot client because
 * there is not much point in having a large amount game hosts simultaneously like a kahoot client would
 * the buffer can clear fast with a small amount of clients
 */

public partial class KahootHost
{
    internal async Task ReceiveAsync()
    {
        while (Socket.State == WebSocketState.Open)
        {
            byte[] buffer = ArrayPool<byte>.Shared.Rent(StateObject.BufferSize);

            var result = await Socket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                return;
            }

            await ProcessDataAsync(buffer);

            ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}
