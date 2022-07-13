namespace Kahoot.NET.API.Requests.Login;

/// <summary>
/// Information to send for logging into the game
/// </summary>
internal class LoginInformation : Data
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginInformation"/> struct
    /// </summary>
    public LoginInformation(string name, string gameId, string content)
    {
        Name = name;
        Content = content;
        GameId = gameId;
        Host = Connection.Host;
        Type = Types.Login;
    }

    /// <summary>
    /// The host of the request
    /// </summary>
    [JsonPropertyName("host")]
    public string Host { get; set; }

    /// <summary>
    /// The username of the client
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// The content embedded within the message
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }

    /// <summary>
    /// The identifier of the game (game code)
    /// </summary>
    [JsonPropertyName("gameid")]
    public string GameId { get; set; }
}
