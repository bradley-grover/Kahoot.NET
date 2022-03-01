namespace Kahoot.NET.Internal.Data.Shared;

/// <summary>
/// Live handshake advice from the kahoot server
/// </summary>
internal class LiveHandshakeAdvice : Advice
{
    /// <summary>
    /// Reconnect, this will always be "retry"
    /// </summary>
    [JsonPropertyName("reconnect")]
    public string? Reconnect { get; set; }
}
