namespace Kahoot.NET.API.Shared;

/// <summary>
/// Represents a screen object as a struct, because this is a cheap object
/// </summary>
public struct Screen
{
    /// <summary>
    /// The length of the user's screen
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// The height of the user's screen
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// The default screen size 1920x1080
    /// </summary>
    public static Screen Default => new() { Width = 1920, Height = 1080 };
}
