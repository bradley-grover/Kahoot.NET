namespace Kahoot.NET.Client;

/// <summary>
/// Configuration for <see cref="KahootClient"/>
/// </summary>
public class KahootClientConfig
{
    /// <summary>
    /// Default <see cref="KahootClientConfig"/> used if the config passed is <see langword="null"></see>
    /// </summary>
    
    public static KahootClientConfig Default { get; } = new()
    {
        EnableReconnect = true,
        ReadOnlyData = true,
    };
    /// <summary>
    /// If the client can invoke a reconnect
    /// </summary>

    [JsonPropertyName("enableReconnect")]
    public bool EnableReconnect { get; set; }

    /// <summary>
    /// Use this setting to see data that is only really used for viewing purposes
    /// </summary>
    [JsonPropertyName("useReadOnlyData")]
    public bool ReadOnlyData { get; set; }
}
