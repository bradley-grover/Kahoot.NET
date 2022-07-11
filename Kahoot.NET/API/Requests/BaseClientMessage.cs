using Kahoot.NET.API.Shared;

namespace Kahoot.NET.API.Requests;

internal class BaseClientMessage : Message
{
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
}
