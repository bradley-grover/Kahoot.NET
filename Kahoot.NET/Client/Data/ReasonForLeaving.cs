namespace Kahoot.NET.Client.Data;

/// <summary>
/// The reason the client left the game
/// </summary>
public enum ReasonForLeaving
{
    /// <summary>
    /// The user requested (you) that the client should leave the game
    /// </summary>
    UserRequested,
    /// <summary>
    /// The game is locked and you can't join
    /// </summary>
    GameLocked,
    /// <summary>
    /// The host kicked you
    /// </summary>
    UserKicked,
    /// <summary>
    /// The queue is full so we cannot join
    /// </summary>
    QueueFull
}
