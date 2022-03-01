namespace Kahoot.NET.Internal.Data.Messages;

internal class LiveBaseMessage
{
    [JsonPropertyName("channel")]
    public string? Channel { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
