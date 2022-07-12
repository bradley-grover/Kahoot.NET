namespace Kahoot.NET.API.Shared;

internal struct Device
{
    [JsonPropertyName("userAgent")]
    public string UserAgent { get; set; }

    [JsonPropertyName("screen")]
    public Screen Screen { get; set; }
}
