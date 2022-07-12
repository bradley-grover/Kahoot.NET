using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Requests;

/// <summary>
/// Lets the server know that the client is still connected
/// </summary>
internal class Packet : ClientMessage
{
    [JsonPropertyName("ext")]
    public ExtWithTimesync<long>? Ext { get; set; }
}
