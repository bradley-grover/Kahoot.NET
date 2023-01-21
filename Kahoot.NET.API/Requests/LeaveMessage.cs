using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Requests;

/// <summary>
/// Message to send when disconnecting from the server
/// </summary>
public class LeaveMessage : BaseClientMessage
{
#nullable disable
    /// <summary>
    /// Initializes a new instance of the <see cref="LeaveMessage"/> class
    /// </summary>
    public LeaveMessage()
    {
        Channel = Channels.Disconnection;
    }

    /// <summary>
    /// Extra data with only the timesync included in it
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtOnlyTimesync Ext { get; set; }
}
