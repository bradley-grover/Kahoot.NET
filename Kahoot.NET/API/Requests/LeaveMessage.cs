using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Requests;

/// <summary>
/// Message to send when disconnecting from the server
/// </summary>
internal class LeaveMessage : BaseClientMessage
{
#nullable disable
    public LeaveMessage()
    {
        Channel = Channels.Disconnection;
    }

    [JsonPropertyName("ext")]
    public ExtOnlyTimesync Ext { get; set; }
}
