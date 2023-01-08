namespace Kahoot.NET.Client.Events;

/// <summary>
/// Event data for the result of <see cref="IKahootClient.JoinAsync(int, string, CancellationToken)"></see> and the event <see cref="IKahootClient.Joined"/>
/// </summary>
public sealed class JoinEventArgs : EventArgs
{
    /// <summary>
    /// If joining the game was a success
    /// </summary>
    public bool IsSuccess => Result == JoinResult.Success;

    /// <summary>
    /// The result of the join operation
    /// </summary>
    public JoinResult Result { get; set; }

    /// <summary>
    /// The code of the game
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JoinEventArgs"/> class
    /// </summary>
    /// <param name="result"></param>
    /// <param name="code"></param>
    public JoinEventArgs(JoinResult result, int code = default)
    {
        Result = result;
        Code = code;
    }
}
