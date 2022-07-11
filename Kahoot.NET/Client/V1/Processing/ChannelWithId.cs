using Kahoot.NET.API;
using Kahoot.NET.API.Requests;
using Kahoot.NET.API.Requests.Json;
namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ProcessChannelIdAsync(string data, int id, string channel)
    {
        switch ((id, channel))
        {
            case (1, Channels.Handshake):
                var hsResponse = JsonSerializer.Deserialize(data, BaseCMessageContext.Default.BaseClientMessage)!;

                if (!await AlertOnNull(hsResponse)) {
                    return;
                }

                State.clientId = hsResponse.ClientId;

                Interlocked.Increment(ref State.id);
                await SendAsync(CreateHS2(), CH2Context.Default.SecondClientHandshake);

                break;
            case (2, Channels.Connection):
                Interlocked.Increment(ref State.id);
                Interlocked.Increment(ref State.ack);

                await SendAsync(CreateLastHS(), LastHsContext.Default.LastHs);
                await Task.Delay(800);

                break;
            default: 
                await ProcessChannelAsync(data, channel);
                break;
        }
    }
}
