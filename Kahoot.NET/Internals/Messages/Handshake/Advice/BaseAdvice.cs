namespace Kahoot.NET.Internals.Messages.Handshake.Advice;

/// <summary>
/// Base advice model for server timings
/// </summary>
public abstract class BaseAdvice
{
    /// <summary>
    /// The interval for the server
    /// </summary>
    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    /// <summary>
    /// The timeout time
    /// </summary>

    [JsonPropertyName("timeout")]
    public int TimeOut { get; set; }
}
