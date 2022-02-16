namespace Kahoot.NET.Internals.Messages.Handshake.Ext;

#nullable disable

/// <summary>
/// Represents Ext with only a time sync
/// </summary>
public class ExtJustTimesync<TTimesync>
{
    /// <summary>
    /// Timesync field
    /// </summary>
    [JsonPropertyName("timesync")]
    public TTimesync Timesync { get; set; }
}
