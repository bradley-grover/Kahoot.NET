namespace Kahoot.NET.API.Shared;

internal struct Screen
{
    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// The default screen size 1920x1080
    /// </summary>
    public static Screen Default => new() { Width = 1920, Height = 1080 };
}
