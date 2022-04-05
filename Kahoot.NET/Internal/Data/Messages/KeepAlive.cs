using Kahoot.NET.Internal.Data.Shared.Ext;
using Kahoot.NET.Internal.Data.Shared.Timesync;

namespace Kahoot.NET.Internal.Data.Messages;

/// <summary>
/// Heartbeat message for the client
/// </summary>
internal class KeepAlive : ExtendedLiveBaseMessage
{
    [JsonPropertyName("ext")]
    public ExtWithTimesync<TimesyncData>? Ext { get; set; }
}
