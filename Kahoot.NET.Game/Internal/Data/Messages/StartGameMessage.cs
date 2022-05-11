using Kahoot.NET.Internal.Data.Messages;

namespace Kahoot.NET.Game.Internal.Data.Messages;

#nullable disable

internal class StartGameMessage : LiveMessage
{
    [JsonPropertyName("data")]
    public StartData Data { get; set; }

    [JsonPropertyName("ext")]
    public object Ext { get; } = new();
}
