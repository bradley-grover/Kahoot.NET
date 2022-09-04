namespace Kahoot.NET.Client;

/// <summary>
/// Client to connect to a Kahoot
/// </summary>
public interface IKahootClient : IDisposable
{
    /// <summary>
    /// When the client has failed to or joined a game, make sure to check if <see cref="JoinEventArgs.Success"/> is true before accessing fields
    /// </summary>
    event Func<object?, JoinEventArgs, Task>? Joined;

    /// <summary>
    /// When the client encounters an error
    /// </summary>
    event Func<object?, ClientErrorEventArgs, Task>? ClientError;

    /// <summary>
    /// When the client has left the game/socket
    /// </summary>
    event Func<object?, LeftEventArgs, Task>? Left;

    /// <summary>
    /// When the client has received a question
    /// </summary>
    event Func<object?, QuestionReceivedEventArgs, Task> QuestionReceived;

    /// <summary>
    /// Join a kahoot game with the name and code
    /// </summary>
    /// <param name="code">The code of the game</param>
    /// <param name="username">The username of the user to join the game, cannot be null</param>
    /// <param name="cancellationToken">A cancellation token to cancel the task</param>
    /// <returns>Whether the game was found or not</returns>
    Task<bool> JoinAsync(int code, string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disconnects the client from the Kahoot game
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the task</param>
    /// <returns>Awaitable</returns>
    Task LeaveAsync(CancellationToken cancellationToken = default);
}
