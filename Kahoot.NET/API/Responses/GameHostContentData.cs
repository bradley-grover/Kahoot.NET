namespace Kahoot.NET.API.Responses;

/// <summary>
/// <see cref="GameContentData"/> with an additional 'host' property
/// </summary>
public class GameHostContentData : GameContentData
{
    /// <summary>
    /// The URI host
    /// </summary>
    [JsonPropertyName("host")]
    public string Host { get; set; } = ConnectionInfo.Host;
}
