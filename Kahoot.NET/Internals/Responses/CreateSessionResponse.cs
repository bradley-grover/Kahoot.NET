namespace Kahoot.NET.Internals.Responses;

/// <summary>
/// JSON response for data used to create the session
/// </summary>
internal class CreateSessionResponse
{
    [JsonPropertyName("gameMode")]
    public string? GameMode { get; set; }

    [JsonPropertyName("collaborations")]
    public bool Collabs { get; set; }

    [JsonPropertyName("liveGameId")]
    public string? LiveGameId { get; set; }

    [JsonPropertyName("challenge")]
    public string? Challenge { get; set; }

    [JsonPropertyName("twoFactorAuth")]
    public bool TwoFactorAuthentication { get; set; }

    [JsonPropertyName("namerator")]
    public bool Namerator { get; set; }

    [JsonPropertyName("smartPractice")]
    public bool SmartPractice { get; set; }

    [JsonPropertyName("participantId")]
    public bool ParticipantId { get; set; }
}
