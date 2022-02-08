namespace Kahoot.NET.Shared;

/// <inheritdoc></inheritdoc>
public class Nemesis : INemesis
{
    /// <inheritdoc></inheritdoc>
    public string? Name { get; set; }
    /// <inheritdoc></inheritdoc>
    public int? Score { get; set; } = 0;
    /// <inheritdoc></inheritdoc>
    public bool? IsGhost { get; set; }
}
