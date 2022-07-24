using Kahoot.NET.API.Shared.Adv;
using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Requests.Handshake;

/// <summary>
/// Handshake used to initialize connection to the server
/// </summary>
public class ClientHandshake : Message
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientHandshake"/> class
    /// </summary>
    public ClientHandshake()
    {
        Channel = Channels.Handshake;
    }
    /// <summary>
    /// Advice to use for the websocket connection
    /// </summary>
    [JsonPropertyName("advice")]
    public Advice? Advice { get; set; } = new()
    {
        Interval = 0,
        Timeout = 60_000
    };

    /// <summary>
    /// The minimum version of the handshake
    /// </summary>

    [JsonPropertyName("minimumVersion")]
    public string? MinimumVersion { get; set; } = Connection.MinVersion;

    /// <summary>
    /// The version of the handshake
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; set; } = Connection.Version;

    /// <summary>
    /// The connection types that our websocket supports
    /// </summary>
    [JsonPropertyName("supportedConnectionTypes")]
    public string[]? SupportedConnectionTypes { get; set; } = Connection.SupportedConnectionTypes.ToArray();

    /// <summary>
    /// Ext data for the client, this also includes timesync data with default values
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtWithTimesync<bool>? Ext { get; set; }
}
