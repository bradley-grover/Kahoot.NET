namespace Kahoot.NET.Internals.Messages.Time;

/// <summary>
/// The first time the server replies with livetimesync data
/// </summary>
public class LiveTimeSyncDataServerFirst
{
    /// <summary>
    /// The P component of the <see cref="LiveTimeSyncDataServerFirst"/>
    /// </summary>
    [JsonPropertyName("p")]
    public int P { get; set; }
    /// <summary>
    /// The A component of the <see cref="LiveTimeSyncDataServerFirst"/>
    /// </summary>
    [JsonPropertyName("a")]
    public int A { get; set; }


    /// <summary>
    /// The TC component of the <see cref="LiveTimeSyncDataServerFirst"/>
    /// </summary>

    [JsonPropertyName("tc")]
    public long Tc { get; set; }

    /// <summary>
    /// The TS component of the <see cref="LiveTimeSyncDataServerFirst"/>
    /// </summary>
    [JsonPropertyName("ts")]
    public long Ts { get; set; }
}
