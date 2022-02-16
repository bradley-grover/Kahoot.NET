using Kahoot.NET.Internals.Messages;
using Kahoot.NET.Internals.Messages.Handshake.Advice;
using Kahoot.NET.Internals.Messages.Handshake.Ext;
using Kahoot.NET.Internals.Messages.Time;

namespace Kahoot.NET.Internals.Responses;

#nullable disable

/// <summary>
/// The first response the client sends back
/// </summary>
public class FirstServerResponse : FirstMessage
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("ext")]
    public FirstServerExt Ext { get; set; }
    /// <summary>
    /// Client Id of the websocket session
    /// </summary>
    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }

    /// <summary>
    /// Id of the response
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// Whether the first message was successful or not
    /// </summary>

    [JsonPropertyName("successful")]
    public bool Successful { get; set; }
    

    /// <summary>
    /// Advice from the websocket server on connection
    /// </summary>

    [JsonPropertyName("advice")]
    public ServerAdvice Advice { get; set; }
}
