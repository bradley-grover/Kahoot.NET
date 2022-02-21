namespace Kahoot.NET.Client.InternalConnectionState;

/// <summary>
/// This displays the current session and shows us if certain events have happened yet
/// </summary>
internal class CurrentSession
{
    public bool FirstMessageReceivedBack { get; set; } = false;

    public int CurrentRequestPosition { get; set; }

    public (int L, int O)? LiveTimesyncData { get; set; }

    public int AcknowledgedCounter { get; set; } = 0;
    public int IdCounter { get; set; } = 0;
}
