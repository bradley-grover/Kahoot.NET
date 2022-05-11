using Kahoot.NET.Internal.Data.Shared.Timesync;

namespace Kahoot.NET.Internal.Data.Shared.Ext;

#nullable disable

internal class ExtOnlyTimesync
{
    [JsonPropertyName("timesync")]
    public TimesyncData Timesync { get; set; }
}
