namespace Kahoot.NET.Internal.Data.Shared.Timesync;

internal class ExtendedTimesyncData : TimesyncData
{
    [JsonPropertyName("ts")]
    public long Ts { get; set; }
}
