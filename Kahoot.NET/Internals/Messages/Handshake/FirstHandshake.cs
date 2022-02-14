using Kahoot.NET.Internals.Messages.Handshake.Advice;
using Kahoot.NET.Internals.Messages.Handshake.Ext;

namespace Kahoot.NET.Internals.Messages.Handshake;

#nullable disable

/// <summary>
/// Used to initialize a new handshake
/// </summary>
public class FirstHandshake : FirstMessage
{
    /// <summary>
    /// Intializes a new instance of the <see cref="FirstHandshake"/> class
    /// </summary>
    [JsonConstructor]
    public FirstHandshake()
    {
        Channel = MessageChannels.Handshake;
        MinimumVersion = InternalConsts.MinVersion;
        Version = InternalConsts.Version;
        ConnectionTypes = InternalConsts.SupportedConnectionTypes;
    }
    /// <summary>
    /// Advice for server timings
    /// </summary>
    [JsonPropertyName("advice")]
    public ClientAdvice Advice { get; set; } = ClientAdvice.Default;

    /// <summary>
    /// Timesync data
    /// </summary>
    [JsonPropertyName("ext")]
    public FirstExt Ext { get; set; } = new();

    /// <summary>
    /// The sent message counter
    /// </summary>

    [JsonPropertyName("id")]
    public string Id { get; set; } = 1.ToString();
}
