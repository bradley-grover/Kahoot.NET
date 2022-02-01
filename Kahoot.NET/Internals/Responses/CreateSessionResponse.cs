namespace Kahoot.NET.Internals.Responses;

internal class CreateSessionResponse
{
    [JsonPropertyName("gameMode")]
    public string? GameMode { get; set; }

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

    public override string ToString()
    {
        return $"{GameMode}";
    }
}
