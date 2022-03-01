namespace Kahoot.NET.Internal.Data.Shared;

internal class Advice : IntervalAdvice
{
    [JsonPropertyName("timeout")]
    public long Timeout { get; set; }
}
