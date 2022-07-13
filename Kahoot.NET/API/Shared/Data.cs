namespace Kahoot.NET.API.Shared;

/// <summary>
/// Represents the data that could be contained in websocket messages
/// </summary>
/// <remarks>
/// This is set as a nullable reference type as it might not exist within a <see cref="Message{TData}"/> but if it
/// exists <see cref="Type"/> must exist
/// </remarks>
internal class Data
{
#nullable disable
    /// <summary>
    /// The type of data that is contained within the data json field this can never be null if data field exists
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }
}
