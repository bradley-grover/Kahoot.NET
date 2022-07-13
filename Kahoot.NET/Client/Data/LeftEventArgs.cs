namespace Kahoot.NET.Client.Data;

/// <summary>
/// Occurs when the client leaves/disconnects
/// </summary>
public class LeftEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LeftEventArgs"/> class
    /// </summary>
    public LeftEventArgs(ReasonForLeaving reason)
    {
        Reason = reason;
    }

    /// <summary>
    /// The reason for why the client has left the game
    /// </summary>
    public ReasonForLeaving Reason { get; set; } = ReasonForLeaving.UserRequested;
}
