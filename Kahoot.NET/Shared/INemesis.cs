namespace Kahoot.NET.Shared;

/// <summary>
/// Represents the current Nemesis of the Client
/// </summary>
public interface INemesis
{
    /// <summary>
    /// If the nemesis is a ghost player or not
    /// </summary>
    bool? IsGhost { get; set; }
    /// <summary>
    /// The nemesis's name
    /// </summary>
    string? Name { get; set; }
    /// <summary>
    /// The current score of the nemesis
    /// </summary>
    int? Score { get; set; }
}
