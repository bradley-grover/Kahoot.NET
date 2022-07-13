namespace Kahoot.NET.API.Shared;

/// <summary>
/// Represents a user's device information there is logic so it is a cheap object
/// </summary>
internal struct Device
{
    /// <summary>
    /// The user agent's of the user's browser
    /// </summary>
    [JsonPropertyName("userAgent")]
    public string UserAgent { get; set; }

    /// <summary>
    /// The screen of the user
    /// </summary>
    [JsonPropertyName("screen")]
    public Screen Screen { get; set; }
}
