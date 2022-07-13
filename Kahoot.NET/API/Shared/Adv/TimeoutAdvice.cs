namespace Kahoot.NET.API.Shared.Adv;

/// <summary>
/// Advice for the timeout of the connection
/// </summary>
public class TimeoutAdvice
{
    /// <summary>
    /// Timeout time for the connection
    /// </summary>
    [JsonPropertyName("timeout")]
    public long Timeout { get; set; }
}
