namespace Kahoot.NET.API.Shared.Extra;

/// <summary>
/// Extra data with only timetrack included
/// </summary>
public struct ExtWithTimetrack
{
    /// <summary>
    /// Unix milliseconds timestamp
    /// </summary>
    [JsonPropertyName("timetrack")]
    public long Timetrack { get; set; }
}
