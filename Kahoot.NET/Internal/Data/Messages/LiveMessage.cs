namespace Kahoot.NET.Internal.Data.Messages;

#nullable disable

internal class LiveMessage : LiveBaseMessage
{
    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }
}
