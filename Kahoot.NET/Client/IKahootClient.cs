namespace Kahoot.NET.Client;

/// <summary>
/// Client used to connect and interact to a Kahoot!
/// </summary>
public interface IKahootClient : IDisposable
{
    /// <summary>
    /// Status of whether a client is in a game or not
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Event triggered when the <see cref="JoinAsync(int, string, CancellationToken)"/> action completes its task
    /// </summary>
    event Func<object?, JoinEventArgs, Task> Joined;

    /// <summary>
    /// Event triggered when the client leaves the game
    /// </summary>
    event Func<object?, LeftEventArgs, Task> Left;

    /// <summary>
    /// The client begins to join the game and will report its results to the delegate property
    /// </summary>
    /// <param name="gameCode"></param>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> JoinAsync(int gameCode, string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disconnects the client from the Kahoot! game
    /// </summary>
    /// <returns></returns>
    Task LeaveAsync();
}
