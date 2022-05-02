using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Messages.Leaving;

#nullable disable

internal class LiveLeaveMessage : LiveMessage
{
    [JsonPropertyName("ext")]
    public ExtWithTimesync<long> Ext { get; set; }
}
