using Kahoot.NET.Internal.Data.Shared;
using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Messages.Handshake;

/// <summary>
/// First handshake that we send to the server
/// </summary>
internal class LiveClientHandshake : LiveBaseMessage
{
    /// <summary>
    /// Advice to use for the websocket connection
    /// </summary>
    [JsonPropertyName("advice")]
    public Advice? Advice { get; set; }
    
    /// <summary>
    /// The minimum version of the handshake
    /// </summary>

    [JsonPropertyName("minimumVersion")]
    public string? MinimumVersion { get; set; }

    /// <summary>
    /// The version of the handshake
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; set; }

    /// <summary>
    /// The connection types that our websocket supports
    /// </summary>
    [JsonPropertyName("supportedConnectionTypes")]
    public string[]? SupportedConnectionTypes { get; set; }

    /// <summary>
    /// Ext data for the client, this also includes timesync data with default values
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtWithTimesync<bool>? Ext { get; set; }
}
