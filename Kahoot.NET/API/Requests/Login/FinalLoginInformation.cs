using Kahoot.NET.API.Shared;

namespace Kahoot.NET.API.Requests.Login;

internal class FinalLoginInformation : Data
{
    public FinalLoginInformation(int gameId, string content)
    {
        Type = Types.Message;
        GameId = gameId;
        Host = Connection.Host;
        Content = content;
        Id = 16;
    }

    [JsonPropertyName("gameid")]
    public int GameId { get; set; }

    [JsonPropertyName("host")]
    public string Host { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }
}
