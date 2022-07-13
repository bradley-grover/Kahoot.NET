namespace Kahoot.NET.API.Shared.Time;

internal class ExtendedTimesync : Timesync
{
    [JsonPropertyName("ts")]
    public long Ts { get; set; }
}
