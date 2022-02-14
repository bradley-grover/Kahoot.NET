namespace Kahoot.NET.Internals.Messages.Handshake.Advice;

#nullable disable

/// <summary>
/// Servers advice on connection
/// </summary>
public class ServerAdvice : BaseAdvice
{
    /// <summary>
    /// Clients reconnection to server - their advice
    /// </summary>
    [JsonPropertyName("reconnect")]
    public string Reconnect { get; set; }
}
