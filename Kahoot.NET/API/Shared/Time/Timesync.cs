namespace Kahoot.NET.API.Shared.Time;


/// <summary>
/// Timesync data to be used to sync the server and the client
/// </summary>
internal class Timesync
{
    /// <summary>
    /// The lag between the server and the client
    /// </summary>
    [JsonPropertyName("l")]
    public long L { get; set; }

    /// <summary>
    /// Time offset
    /// </summary>
    [JsonPropertyName("o")]
    public long O { get; set; }

    /// <summary>
    /// The current time in UTC milliseconds
    /// </summary>
    [JsonPropertyName("tc")]
    public long CurrentTime { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}
