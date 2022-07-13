namespace Kahoot.NET.API.Responses;

/// <summary>
/// Status response used for the channel <see cref="Channels.Status"/>
/// </summary>
public class StatusResponse : Data
{
    /// <summary>
    /// The status of the certain event
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
