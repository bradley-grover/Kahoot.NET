using Kahoot.NET.API.Shared.Time;

namespace Kahoot.NET.API.Shared.Extra;

#nullable disable

/// <summary>
/// Represents extra data with only timesync included
/// </summary>
public class ExtOnlyTimesync
{
    /// <summary>
    /// The timesync data included in the object
    /// </summary>
    [JsonPropertyName("timesync")]
    public Timesync Timesync { get; set; }
}

