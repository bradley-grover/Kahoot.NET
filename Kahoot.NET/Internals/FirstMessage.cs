using Kahoot.NET.Internals.Messages;

#nullable disable

namespace Kahoot.NET.Internals;

/// <summary>
/// The first message for the client and the server
/// </summary>
public abstract class FirstMessage : LiveBaseMessageResponse
{
    /// <summary>
    /// The minimum version
    /// </summary>
    [JsonPropertyName("minimumVersion")]
    public string MinimumVersion { get; set; }

    /// <summary>
    /// The current version
    /// </summary>

    [JsonPropertyName("version")]
    public string Version { get; set; }
    /// <summary>
    /// The types of connections the websocket supports
    /// </summary>
    [JsonPropertyName("supportedConnectionTypes")]
    public string[] ConnectionTypes { get; set; }
}
