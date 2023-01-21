namespace Kahoot.NET.API.Shared;

public class Video
{
    [JsonPropertyName("startTime")]
    public int StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public int EndTime { get; set; }

#nullable disable

    [JsonPropertyName("service")]
    public string Service { get; set; }

    [JsonPropertyName("fullUrl")]
    public string FullUrl { get; set; }

#nullable disable
}
