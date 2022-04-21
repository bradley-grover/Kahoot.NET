namespace Kahoot.NET.Internal.Data.Messages.Login;

#nullable disable

internal class LoginData
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "login";

    [JsonPropertyName("gameid")]
    public string GameId { get; set; }

    [JsonPropertyName("host")]
    public string Host { get; set; } = "kahoot.it";

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }
}
