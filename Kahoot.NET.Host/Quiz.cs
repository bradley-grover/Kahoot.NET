namespace Kahoot.NET.Host;

/// <summary>
/// Represents the quiz that the host is currently hosting
/// </summary>
public class Quiz
{
    public uint Code { get; private set; }
    public bool Locked { get; private set; }
}
