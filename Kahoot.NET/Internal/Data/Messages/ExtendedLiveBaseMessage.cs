namespace Kahoot.NET.Internal.Data.Messages;

internal class ExtendedLiveBaseMessage : LiveBaseMessage
{
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    [JsonPropertyName("connectionType")]
    public string? ConnectionType { get; set; }
}
