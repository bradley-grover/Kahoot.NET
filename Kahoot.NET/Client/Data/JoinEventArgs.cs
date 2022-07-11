namespace Kahoot.NET.Client.Data;

/// <summary>
/// The result of a user trying to join a game
/// </summary>
public class JoinEventArgs : EventArgs
{
    /// <summary>
    /// If joining the game was a success
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The game requires 2FA to join
    /// </summary>
    public bool Requires2Fa { get; set; }
}
