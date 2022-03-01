namespace Kahoot.NET.Internal.Data.Shared;

internal class LiveHandshakeAdvice : Advice
{
    [JsonPropertyName("reconnect")]
    public string? Reconnect { get; set; }
}
