using Kahoot.NET.API.Shared;

namespace Kahoot.NET.API.Requests.Login;

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

    [JsonPropertyName("host")]
    public string Host { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("gameid")]
    public string GameId { get; set; }
}
