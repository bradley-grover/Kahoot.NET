namespace Kahoot.NET.API.Shared.Adv;

/// <summary>
/// Base advice for server connection timings
/// </summary>
public class BaseAdvice
{
    /// <summary>
    /// Interval between requests
    /// </summary>
    [JsonPropertyName("interval")]
    public long Interval { get; set; }
}
