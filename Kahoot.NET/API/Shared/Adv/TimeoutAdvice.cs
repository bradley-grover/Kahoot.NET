namespace Kahoot.NET.API.Shared.Adv;

internal class TimeoutAdvice
{
    /// <summary>
    /// Timeout time for the connection
    /// </summary>
    [JsonPropertyName("timeout")]
    public long Timeout { get; set; }
}
