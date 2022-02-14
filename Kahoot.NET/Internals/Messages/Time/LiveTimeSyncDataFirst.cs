namespace Kahoot.NET.Internals.Messages.Time;

/// <summary>
/// Represents lag and timing of connection
/// </summary>
public class LiveTimeSyncDataFirst
{
    /// <summary>
    /// Represents the L component of the <see cref="LiveTimeSyncDataFirst"/>
    /// </summary>
    [JsonPropertyName("l")]
    public int L { get; set; } = 0;

    /// <summary>
    /// Represents the O component of the <see cref="LiveTimeSyncDataFirst"/>
    /// </summary>
    [JsonPropertyName("o")]
    public int O { get; set; } = 0;

    /// <summary>
    /// Current time
    /// </summary>
    [JsonPropertyName("tc")]
    public long CurrentTime { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}
