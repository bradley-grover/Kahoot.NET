using Kahoot.NET.Internals.Messages.Time;

#nullable disable

namespace Kahoot.NET.Internals.Messages.Handshake.Ext;

/// <summary>
/// General ext which includes ack/timesync
/// </summary>
public class GeneralExt
{
    /// <summary>
    /// Acknowledged number
    /// </summary>
    [JsonPropertyName("ack")]
    public int Ack { get; set; }

    /// <summary>
    /// Timesynd data
    /// </summary>
    [JsonPropertyName("timesync")]
    public  LiveTimeSyncDataFirst Timesync { get; set; }
}
