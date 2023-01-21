namespace Kahoot.NET.API.Responses;

#nullable disable

/// <summary>
/// Sends the <see cref="ContentData"/> with the game code included as a property
/// </summary>
public class GameContentData : ContentData
{
    /// <summary>
    /// Represents the game code, is already known so not that worth answering
    /// </summary>
    [JsonPropertyName("gameid")]
    public string GameId { get; set; }
}
