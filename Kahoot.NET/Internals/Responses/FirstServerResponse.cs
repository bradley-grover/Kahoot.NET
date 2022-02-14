using Kahoot.NET.Internals.Messages;

namespace Kahoot.NET.Internals.Responses;

#nullable disable

/// <summary>
/// The first response the client sends back
/// </summary>
public class FirstServerResponse : FirstMessage
{
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
}
