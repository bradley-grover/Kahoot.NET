namespace Kahoot.NET.Client.Events;

/// <summary>
/// The condition on which the client left on, note that this not apart of the Kahoot! API
/// </summary>
public enum LeaveCondition
{
    /// <summary>
    /// The client left using <see cref="IKahootClient.LeaveAsync"/> or by a forced condition
    /// </summary>
    Requested,

    /// <summary>
    /// The client was kicked from the game by the creator
    /// </summary>
    Kicked,

    /// <summary>
    /// The client is waiting in a queue so it has left
    /// </summary>
    Full,

    /// <summary>
    /// The game is locked by the creator so it cannot be joined
    /// </summary>
    Locked,

    /// <summary>
    /// The client failed to join the game so is closing the socket
    /// </summary>
    JoinFailure
}
