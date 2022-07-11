using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Requests.Handshake;

internal class LastHs : ClientMessage
{
    public LastHs()
    {
        Channel = Channels.Connection;
    }

    [JsonPropertyName("ext")]
    public ExtWithTimesync<long>? Ext { get; set; }
}
