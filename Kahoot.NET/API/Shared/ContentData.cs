namespace Kahoot.NET.API.Shared;

#nullable disable

/// <summary>
/// Represents data where it contains an integer id and content that could be a JSON string within the content field to further deserialize
/// </summary>
public class ContentData : Data
{
    /// <summary>
    /// Integer identifer for the data
    /// </summary>
    [JsonPropertyName("id")]
    public uint Id { get; set; }

    /// <summary>
    /// JSON content embedded within the message
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }
}
