namespace Kahoot.NET.Client.InternalConnectionState;

/// <summary>
/// This displays the current session and shows us if certain events have happened yet
/// </summary>
internal class CurrentSession
{
    public bool FirstMessageReceivedBack { get; set; } = false;

    public int CurrentRequestPosition { get; set; }
}
