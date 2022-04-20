using Kahoot.NET.Internal.Data.Shared;
using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Responses.Handshake;

#nullable disable

/// <summary>
/// Second response from the handshake
/// </summary>
internal class SecondHandshakeResponse : Heartbeat
{
    [JsonPropertyName("ext")]
    public Ext<long> Ext { get; set; }

    [JsonPropertyName("advice")]
    public LiveHandshakeAdvice Advice { get; set; }
}
