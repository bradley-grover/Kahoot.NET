using Kahoot.NET.Internal.Data.Messages;

namespace Kahoot.NET.Game.Internal.Data.Messages;

#nullable disable

internal class StartGameMessage : LiveMessage
{
    [JsonPropertyName("ext")]
    public object Ext { get; set; }

    [JsonPropertyName("data")]
    public object Data { get; set; }
}
