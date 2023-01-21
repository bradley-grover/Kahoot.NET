namespace Kahoot.NET.Client.Events;

/// <summary>
/// Event data for when the client leaves a Kahoot! game
/// </summary>
public sealed class LeftEventArgs : EventArgs
{
    /// <summary>
    /// The condition on which the client left for
    /// </summary>
    public LeaveCondition Condition { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LeftEventArgs"/> class
    /// </summary>
    public LeftEventArgs(LeaveCondition condition)
    {
        Condition = condition;
    }
}
