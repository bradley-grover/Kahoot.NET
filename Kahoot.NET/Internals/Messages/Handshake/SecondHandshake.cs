using Kahoot.NET.Internals.Messages.Handshake.Advice;
using Kahoot.NET.Internals.Messages.Handshake.Ext;

#nullable disable

namespace Kahoot.NET.Internals.Messages.Handshake;

/// <summary>
/// The second handshake to send to Kahoot
/// </summary>
public class SecondHandshake : LiveBaseMessageResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SecondHandshake"/> class
    /// </summary>
    public SecondHandshake()
    {
        Channel = MessageChannels.Connection;
    }
    /// <summary>
    /// Ext data for second handshake
    /// </summary>
    [JsonPropertyName("ext")]
    public GeneralExt Ext { get; set; } = new GeneralExt()
    {
        Ack = 0,
        Timesync = new() { }
    };

    /// <summary>
    /// Connection type of this handshake
    /// </summary>
    [JsonPropertyName("connectionType")]
    public string ConnectionType { get; set; } = "websocket";

    /// <summary>
    /// Advice for the connection
    /// </summary>
    [JsonPropertyName("advice")]
    public TimeOutAdvice Advice { get; set; }
    
    /// <summary>
    /// Client Id of the request
    /// </summary>
    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }

    /// <summary>
    /// Id of the request
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = "2";
}
