using Kahoot.NET.Internal.Data.Shared;
using Kahoot.NET.Internal.Data.Shared.Ext;
using Kahoot.NET.Internal.Data.Messages.Handshake;

namespace Kahoot.NET.Internal.Data.Responses.Handshake;

/// <summary>
/// Response we get from kahoot after sending a <see cref="LiveClientHandshake"/>
/// </summary>
internal class LiveClientHandshakeResponse : LiveBaseMessage
{
    /// <summary>
    /// The client id used for the connection
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// The connection types supported for the connection
    /// </summary>
    [JsonPropertyName("supportedConnectionTypes")]
    public string[]? SupportedConnectionTypes { get; set; }

    /// <summary>
    /// The minimum version to be used
    /// </summary>
    [JsonPropertyName("minimumVersion")]
    public string? MinimumVersion { get; set; }

    /// <summary>
    /// The version being used
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; set; }

    /// <summary>
    /// Advice for the connection sent by the server
    /// </summary>
    [JsonPropertyName("advice")]
    public LiveHandshakeAdvice? Advice { get; set; }
    /// <summary>
    /// 
    /// </summary>
#nullable disable
    [JsonPropertyName("timesync")]
    public ExtWithExtendedTimesyncData Ext { get; set; }
}
