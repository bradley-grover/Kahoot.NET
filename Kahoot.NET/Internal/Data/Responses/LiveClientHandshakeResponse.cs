using Kahoot.NET.Internal.Data.Shared;

namespace Kahoot.NET.Internal.Data.Responses;

internal class LiveClientHandshakeResponse : LiveBaseMessage
{
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    [JsonPropertyName("supportedConnectionTypes")]
    public string[]? SupportedConnectionTypes { get; set; }

    [JsonPropertyName("minimumVersion")]
    public string? MinimumVersion { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("advice")]
    public LiveHandshakeAdvice? Advice { get; set; }
}
