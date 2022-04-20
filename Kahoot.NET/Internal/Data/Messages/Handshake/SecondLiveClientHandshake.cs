using Kahoot.NET.Internal.Data.Shared;
using Kahoot.NET.Internal.Data.Shared.Ext;
using Kahoot.NET.Internal.Data.Responses.Handshake;

namespace Kahoot.NET.Internal.Data.Messages.Handshake;

/// <summary>
/// The second handshake we send to the server after receiving a <see cref="LiveClientHandshakeResponse"/>
/// </summary>
internal class SecondLiveClientHandshake : ExtendedLiveBaseMessage
{ 
    /// <summary>
    /// Advice to send to the server
    /// </summary>
    [JsonPropertyName("advice")]
    public TimeoutAdvice? Advice { get; set; }

    /// <summary>
    /// Ext with timesync data
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtWithTimesync<long>? Ext { get; set; }
}
