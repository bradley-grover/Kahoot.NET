namespace Kahoot.NET.Internal.Data.Shared.Timesync;

internal class TimesyncData
{
    [JsonPropertyName("l")]
    public long L { get; set; }

    [JsonPropertyName("o")]
    public long O { get; set; }

    [JsonPropertyName("tc")]
    public long CurrentTime { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}
