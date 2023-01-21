namespace Kahoot.NET.API.Shared.Adv;

/// <summary>
/// Handshake advice sent during handshake
/// </summary>
public class HSAdvice : Advice
{
    /// <summary>
    /// Reconnect, this will always be "retry"
    /// </summary>
    [JsonPropertyName("reconnect")]
    public string? Reconnect { get; set; }
}
