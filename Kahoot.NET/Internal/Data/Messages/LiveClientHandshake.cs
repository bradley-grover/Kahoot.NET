using Kahoot.NET.Internal.Data.Shared;
using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Messages;

internal class LiveClientHandshake : LiveBaseMessage
{
    [JsonPropertyName("advice")]
    public Advice? Advice { get; set; }

    [JsonPropertyName("minimumVersion")]
    public string? MinimumVersion { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("supportedConnectionTypes")]
    public string[]? SupportedConnectionTypes { get; set; }

    [JsonPropertyName("ext")]
    public ExtWithTimesync<bool>? Ext { get; set; }
}
