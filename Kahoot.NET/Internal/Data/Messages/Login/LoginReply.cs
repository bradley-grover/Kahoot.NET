namespace Kahoot.NET.Internal.Data.Messages.Login;

#nullable disable

internal class LoginReply
{
    [JsonPropertyName("channel")]
    public string Channel { get; set; } = LiveMessageChannels.Service;

    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }

    [JsonPropertyName("data")]
    public object Data { get; set; }

    [JsonPropertyName("ext")]
    public object Ext { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }
}
