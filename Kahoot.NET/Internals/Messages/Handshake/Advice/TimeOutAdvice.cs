namespace Kahoot.NET.Internals.Messages.Handshake.Advice;

/// <summary>
/// Part that includes the timeout advice
/// </summary>
public class TimeOutAdvice
{
    /// <summary>
    /// The timeout used for the websocket
    /// </summary>
    [JsonPropertyName("timeout")]
    public int TimeOut { get; set; }
}
