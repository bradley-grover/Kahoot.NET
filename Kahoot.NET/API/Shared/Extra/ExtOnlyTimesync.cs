using Kahoot.NET.API.Shared.Time;

namespace Kahoot.NET.API.Shared.Extra;

#nullable disable

internal class ExtOnlyTimesync
{
    [JsonPropertyName("timesync")]
    public Timesync Timesync { get; set; }
}

