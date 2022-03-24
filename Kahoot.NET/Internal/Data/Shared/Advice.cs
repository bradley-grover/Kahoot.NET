namespace Kahoot.NET.Internal.Data.Shared;

/// <summary>
/// Connection advice for the server
/// </summary>
internal class Advice : IntervalAdvice
{
    /// <summary>
    /// Timeout time for the connection
    /// </summary>
    [JsonPropertyName("timeout")]
    public long Timeout { get; set; }
}
