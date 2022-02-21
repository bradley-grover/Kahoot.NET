using Kahoot.NET.Internals.Messages.Handshake.Advice;
using Kahoot.NET.Internals.Messages.Handshake.Ext;
using Kahoot.NET.Internals.Messages.Time;

#nullable disable

namespace Kahoot.NET.Internals.Responses;

/// <summary>
/// Represents the first message that kahoot sends back
/// </summary>
public class FirstServerResponse : FirstMessage
{
    /// <summary>
    /// Ext field of response
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtJustTimesync<LiveTimeSyncDataServerFirst> Ext { get; set; }

    /// <summary>
    /// Id for the websocket client
    /// </summary>
    [JsonPropertyName("clientId")] 
    public string ClientId { get; set; }


    /// <summary>
    /// Id of the response
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// If the response is successful or not
    /// </summary>
    [JsonPropertyName("successful")]
    public bool Successful { get; set; }

    /// <summary>
    /// Advice for connection timings
    /// </summary>

    [JsonPropertyName("advice")]
    public ServerAdvice Advice { get; set; }
}
