using System.Text.Json.Serialization;

namespace Kahoot.NET.Host;

/// <summary>
/// Represents a game configuration
/// </summary>
public class GameConfiguration
{
    /// <summary>
    /// If collaborations are enabled
    /// </summary>
    public bool Collaborations { get; set; } = false;

    /// <summary>
    /// If the Kahoot! is team mode
    /// </summary>
    public bool TeamMode { get; set; } = false;

    /// <summary>
    /// If the Kahoot! requires 2 factor authentication
    /// </summary>
    public bool TwoFactorAuth { get; set; } = false;

    /// <summary>
    /// If the Namerator is enabled
    /// </summary>
    public bool Namerator { get; set; } = false;

    /// <summary>
    /// If participant identifier is enabled
    /// </summary>
    public bool ParticipantId { get; set; } = false;

    /// <summary>
    /// If smart practice is enabled
    /// </summary>
    public bool SmartPractice { get; set; } = false;

    /// <summary>
    /// The game mode of the quiz
    /// </summary>
    public string GameMode { get; set; } = "normal";

    /// <summary>
    /// Default game configuration
    /// </summary>
    [JsonIgnore]
    public static GameConfiguration Default => _default;

    private static readonly GameConfiguration _default = new();
}
