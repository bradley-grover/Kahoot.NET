using Kahoot.NET.API.Shared.Time;

namespace Kahoot.NET.API.Shared.Extra;

#nullable disable

internal class ExtWithETC : Ext<bool>
{
    [JsonPropertyName("timesync")]
    public ExtendedTimesync Timesync { get; set; }
}
