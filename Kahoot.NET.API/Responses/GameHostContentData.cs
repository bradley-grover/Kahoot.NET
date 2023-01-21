namespace Kahoot.NET.API.Responses;

public class GameHostContentData : GameContentData
{
    /// <summary>
    /// The URI host
    /// </summary>
    [JsonPropertyName("host")]
    public string Host { get; set; } = ConnectionInfo.Host;
}
