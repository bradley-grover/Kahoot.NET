using Kahoot.NET.Internal.Data.Responses;
using Kahoot.NET.Internal.Data.Shared;
using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Game.Internal.Data.Responses;

#nullable disable

internal class QuizHandshakeResponse : Heartbeat
{
    [JsonPropertyName("ext")]
    public Ext<long> Ext { get; set; }

    [JsonPropertyName("advice")]
    public LiveHandshakeAdvice Advice { get; set; }
}
