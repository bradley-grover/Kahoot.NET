namespace Kahoot.NET.API.Shared;

internal class Message
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
#nullable disable
    [JsonPropertyName("channel")]
    public string Channel { get; set; }
}
