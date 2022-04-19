using Kahoot.NET.Internal.Data.Shared.Timesync;

namespace Kahoot.NET.Internal.Data.Shared.Ext;

#nullable disable

internal class ExtWithExtendedTimesyncData : Ext<bool>
{
    [JsonPropertyName("timesync")]
    public ExtendedTimesyncData Timesync { get; set; }
}
