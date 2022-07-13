namespace Kahoot.NET.API.Shared.Adv;

internal class HSAdvice : Advice
{
    /// <summary>
    /// Reconnect, this will always be "retry"
    /// </summary>
    [JsonPropertyName("reconnect")]
    public string? Reconnect { get; set; }
}
