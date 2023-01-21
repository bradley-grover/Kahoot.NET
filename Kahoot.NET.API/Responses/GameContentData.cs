namespace Kahoot.NET.API.Responses;

// not really needed but why not

#nullable disable

public class GameContentData : ContentData
{
    /// <summary>
    /// Represents the game code, is already known so not that worth answering
    /// </summary>
    [JsonPropertyName("gameid")]
    public string GameId { get; set; }
}
