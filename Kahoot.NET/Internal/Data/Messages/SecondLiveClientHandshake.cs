using Kahoot.NET.Internal.Data.Shared;
using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Messages;

/// <summary>
/// The second handshake we send to the server after receiving a <see cref="LiveClientHandshakeResponse"/>
/// </summary>
internal class SecondLiveClientHandshake : ExtendedLiveBaseMessage
{ 
    /// <summary>
    /// Advice to send to the server
    /// </summary>
    [JsonPropertyName("advice")]
    public IntervalAdvice? Advice { get; set; }

    /// <summary>
    /// Ext with timesync data
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtWithTimesync<long>? Ext { get; set; }
}
