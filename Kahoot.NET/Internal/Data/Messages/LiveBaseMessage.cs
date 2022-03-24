namespace Kahoot.NET.Internal.Data.Messages;

/// <summary>
/// Base of most messages to the kahoot web server
/// </summary>
internal class LiveBaseMessage
{
    /// <summary>
    /// The channel that the message is in, can be any of <see cref="LiveMessageChannels"/>
    /// </summary>
    [JsonPropertyName("channel")]
    public string? Channel { get; set; }

    /// <summary>
    /// The id of the <see cref="LiveBaseMessage"/> this is incremented 
    /// after each request and acknowledgement
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
