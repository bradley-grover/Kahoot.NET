namespace Kahoot.NET.API.Shared.Extra;

internal struct ExtWithTimetrack
{
    [JsonPropertyName("timetrack")]
    public long Timetrack { get; set; }
}
