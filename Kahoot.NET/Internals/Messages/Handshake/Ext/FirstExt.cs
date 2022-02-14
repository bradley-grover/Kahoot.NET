using Kahoot.NET.Internals.Messages.Time;

namespace Kahoot.NET.Internals.Messages.Handshake.Ext;

/// <summary>
/// Important part of sending messages to startup
/// </summary>
public class FirstExt
{
    /// <summary>
    /// If the data has been acknowledged
    /// </summary>
    [JsonPropertyName("ack")]
    public bool Acknowledged { get; set; }

    /// <summary>
    /// Timesync data for kahoot websocket
    /// </summary>
    [JsonPropertyName("timesync")]
    public LiveTimeSyncDataFirst Timesync { get; set; } = new();
}
