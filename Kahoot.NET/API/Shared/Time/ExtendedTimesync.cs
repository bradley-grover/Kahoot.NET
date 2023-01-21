namespace Kahoot.NET.API.Shared.Time;

/// <summary>
/// Represents server time
/// </summary>
public class ExtendedTimesync : Timesync
{
    /// <summary>
    /// Server time in UTC milliseconds
    /// </summary>
    [JsonPropertyName("ts")]
    public long Ts { get; set; }
}
