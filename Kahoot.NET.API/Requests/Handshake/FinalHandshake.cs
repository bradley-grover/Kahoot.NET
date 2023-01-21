using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Requests.Handshake;

/// <summary>
/// The last handshake message to send for the handshake part of the session
/// </summary>
public class FinalHandshake : ClientMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FinalHandshake"/> class
    /// </summary>
    public FinalHandshake()
    {
        Channel = Channels.Connect;
    }

    /// <summary>
    /// Object to hold time related data for syncing to the server
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtWithTimesync<long>? Ext { get; set; }
}
