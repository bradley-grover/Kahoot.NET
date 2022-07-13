using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Requests.Handshake;

/// <summary>
/// The last handshake message to send for the handshake part of the session
/// </summary>
public class LastHs : ClientMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LastHs"/> class
    /// </summary>
    public LastHs()
    {
        Channel = Channels.Connection;
    }

    /// <summary>
    /// Object to hold time related data for syncing to the server
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtWithTimesync<long>? Ext { get; set; }
}
