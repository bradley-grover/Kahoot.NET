namespace Kahoot.NET.Internal.Data.Shared;

/// <summary>
/// Internal advice used for the connection and to inherit from
/// </summary>
internal class IntervalAdvice
{
    /// <summary>
    /// Interval between requests
    /// </summary>
    [JsonPropertyName("interval")]
    public long Interval { get; set; }
}
