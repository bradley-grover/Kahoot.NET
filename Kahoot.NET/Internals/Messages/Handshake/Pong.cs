namespace Kahoot.NET.Internals.Messages.Handshake;

#nullable disable

/// <summary>
/// A pong to kahoot's socket
/// </summary>
public class Pong
{
    /// <summary>
    /// The channel to send the message to
    /// </summary>
    [JsonPropertyName("channel")]
    public string Channel { get; set; } = MessageChannels.Connection;
}
