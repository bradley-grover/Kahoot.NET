namespace Kahoot.NET.Internals.Messages.Handshake.Advice;

/// <summary>
/// Base advice model for server timings
/// </summary>
public abstract class BaseAdvice : TimeOutAdvice
{
    /// <summary>
    /// The interval for the server
    /// </summary>
    [JsonPropertyName("interval")]
    public int Interval { get; set; }
}
