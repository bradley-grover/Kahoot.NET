using Kahoot.NET.API.Requests.Json;
using Kahoot.NET.Client.Data.Errors;

namespace Kahoot.NET.Hosting.Client;

public partial class KahootHost
{
    private async Task ChannelWithIdAsync(string content, int id, string channel, string? dataType = null)
    {
        switch ((id, channel))
        {
            case (1, Channels.Handshake):
                var handshakeResponse = JsonSerializer.Deserialize(content, BaseCMessageContext.Default.BaseClientMessage)!;

                if (handshakeResponse.ClientId is null)
                {
                    await HostError.InvokeEventAsync(this,
                        new(new KahootUnrecoverableException("The client id was not found")));
                }

                State.clientId = handshakeResponse.ClientId;

                Interlocked.Increment(ref State.id);

                await SendAsync(CreateHS2(), CH2Context.Default.SecondClientHandshake);
                break;
            case (2, Channels.Connection):


                break;
            case (_, Channels.Connection):
                Interlocked.Increment(ref State.id);
                Interlocked.Increment(ref State.ack);

                //await SendPacketAsync();
                break;
            default:
                await ChannelAsync(content, channel, dataType);
                break;
        }
    }
}
