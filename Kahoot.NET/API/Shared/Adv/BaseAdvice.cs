namespace Kahoot.NET.API.Shared.Adv;

internal class BaseAdvice
{
    /// <summary>
    /// Interval between requests
    /// </summary>
    [JsonPropertyName("interval")]
    public long Interval { get; set; }
}
