using Kahoot.NET.Internals.Messages.Time;

namespace Kahoot.NET.Internals.Messages.Handshake.Ext;

#nullable disable

/// <summary>
/// The first time the server replies with an ext
/// </summary>
public class FirstServerExt
{
    /// <summary>
    /// If the request was acknowledged or not
    /// </summary>
    [JsonPropertyName("ack")]
    public bool Acknowledged { get; set; }
    /// <summary>
    /// Timesync data for the <see cref="FirstServerExt"/>
    /// </summary>

    [JsonPropertyName("timesync")]
    public LiveTimeSyncDataServerFirst Timesync { get; set; }
}
