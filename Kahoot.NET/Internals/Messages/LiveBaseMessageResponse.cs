namespace Kahoot.NET.Internals.Messages;

#nullable disable

/// <summary>
/// Base of data events
/// </summary>
public abstract class LiveBaseMessageResponse
{
    /// <summary>
    /// Channel which the data gets sent in
    /// </summary>
    [JsonPropertyName("channel")]
    public string Channel { get; set; }
}
