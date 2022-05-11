namespace Kahoot.NET.Game.Internal.Data.Messages;

#nullable disable

internal class StartData
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("gameid")]
    public string GameId { get; set; }

    [JsonPropertyName("host")]
    public string Host { get; set; }
}
