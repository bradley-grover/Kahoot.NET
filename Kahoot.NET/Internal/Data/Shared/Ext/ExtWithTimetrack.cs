namespace Kahoot.NET.Internal.Data.Shared.Ext;

internal struct ExtWithTimetrack
{
    [JsonPropertyName("timetrack")]
    public long Timetrack { get; set; }
}
