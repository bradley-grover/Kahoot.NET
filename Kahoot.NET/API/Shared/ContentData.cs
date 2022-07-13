namespace Kahoot.NET.API.Shared;

#nullable disable

internal class ContentData : Data
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }
}
