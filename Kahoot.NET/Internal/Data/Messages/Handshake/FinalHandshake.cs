using Kahoot.NET.Internal.Data.Responses.Handshake;
using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Messages.Handshake;

#nullable disable

/// <summary>
/// Final handshake to send after receiving <see cref="SecondHandshakeResponse"/>
/// </summary>
internal class FinalHandshake : ExtendedLiveBaseMessage
{
    [JsonPropertyName("ext")]
    public ExtWithTimesync<long> Ext { get; set; }
}
