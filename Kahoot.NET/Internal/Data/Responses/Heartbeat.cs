namespace Kahoot.NET.Internal.Data.Responses;

internal class Heartbeat : LiveBaseMessage
{
    [JsonPropertyName("successful")]
    public bool Successful { get; set; }
}
