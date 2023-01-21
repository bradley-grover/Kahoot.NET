namespace Kahoot.NET.API.Shared;

/// <summary>
/// Represents a playable video by Kahoot!, this API is not exposed publicly by any of the clients but may be useful as an API
/// </summary>
public class Video
{
    /// <summary>
    /// The start time to be played in the video
    /// </summary>
    [JsonPropertyName("startTime")]
    public int StartTime { get; set; }

    /// <summary>
    /// The end time in the video
    /// </summary>
    [JsonPropertyName("endTime")]
    public int EndTime { get; set; }

#nullable disable

    /// <summary>
    /// Represents the service of the video, eg. Youtube
    /// </summary>
    [JsonPropertyName("service")]
    public string Service { get; set; }

    /// <summary>
    /// The full URI for the video to play
    /// </summary>
    [JsonPropertyName("fullUrl")]
    public string FullUrl { get; set; }

#nullable disable
}
