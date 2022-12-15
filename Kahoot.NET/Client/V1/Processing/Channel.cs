namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ProcessChannelAsync(ReadOnlyMemory<byte> content, string channel, string? data = null)
    {
        if (data is null) return;

        switch (channel)
        {
            case Channels.Connect:
                Interlocked.Increment(ref State.id);
                Interlocked.Increment(ref State.ack);

                await SendPacketAsync();
                break;
            case Channels.Status:
                await StatusAsync(content, data);
                break;
            case Channels.Service:
                await ServiceAsync(content, data);
                break;
            case Channels.Player:
                await PlayerAsync(content, data);
                break;
        }
    }
}
