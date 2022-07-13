namespace Kahoot.NET.API.Requests.Login;

/// <summary>
/// Data to be included in when completing the login info that we have to send to authenticate to the server
/// </summary>
public class FinalLoginInformation : Data
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FinalLoginInformation"/>
    /// </summary>
    /// <param name="gameId">The identifier (game code) of the game</param>
    /// <param name="content">The content included within the message</param>
    public FinalLoginInformation(int gameId, string content)
    {
        Type = Types.Message;
        GameId = gameId;
        Host = Connection.Host;
        Content = content;
        Id = 16;
    }

    /// <summary>
    /// The identifier (game code) of the game
    /// </summary>
    [JsonPropertyName("gameid")]
    public int GameId { get; set; }

    /// <summary>
    /// The host of the request
    /// </summary>
    [JsonPropertyName("host")]
    public string Host { get; set; }

    /// <summary>
    /// The identifier of the message
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// The content embedded within the message
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }
}
