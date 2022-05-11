namespace Kahoot.NET.Game.Client;

#nullable disable

public class GameConfiguration
{
    public bool Collaborations { get; set; } = false;
    public bool TeamMode { get; set; } = false;
    public bool TwoFactorAuth { get; set; } = false;
    public bool Namerator { get; set; } = false;
    public bool ParticipantId { get; set; } = false;
    public bool SmartPractice { get; set; } = false;
    public string GameMode { get; set; } = "normal";
}
