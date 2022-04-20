using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Messages;

/// <summary>
/// Heartbeat message for the client
/// </summary>
internal class KeepAlive : ExtendedLiveBaseMessage
{
    [JsonPropertyName("ext")]
    public ExtWithTimesync<long>? Ext { get; set; }
}
