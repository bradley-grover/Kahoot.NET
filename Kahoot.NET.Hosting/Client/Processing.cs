using System.Text;
using Kahoot.NET.API.Shared;
using Kahoot.NET.Extensions;

namespace Kahoot.NET.Hosting.Client;

public partial class KahootHost
{
    internal async Task ProcessDataAsync(ReadOnlyMemory<byte> data)
    {
        string partialJson = Encoding.UTF8.GetString(data.Span);

        string json = new(partialJson.AsSpan(1, partialJson.Length - 2));

        Logger?.LogDebug("[RECEIVE]: {json}", json);

        Message? message = JsonSerializer.Deserialize(json, MessageContext.Default.Message);

        if (message is null)
        {
            return;
        }

        await (message.Id is null ? 
            ChannelAsync(json, message.Channel, message?.Data?.Type) : 
            ChannelWithIdAsync(json, int.Parse(message.Id.AsSpan()), message.Channel, message?.Data?.Type)); 
    }
}
