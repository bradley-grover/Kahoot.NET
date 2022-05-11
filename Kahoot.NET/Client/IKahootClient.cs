namespace Kahoot.NET.Client;

/// <summary>
/// Client to connect to a Kahoot
/// </summary>
/// <remarks>
/// It implements <see cref="IDisposable"/>
/// </remarks>
public interface IKahootClient : IDisposable
{
    /// <summary>
    /// Join a kahoot game with the name and code
    /// </summary>
    /// <param name="code">The code of the game</param>
    /// <param name="username">The username of the user to join the game</param>
    /// <param name="cancellationToken">A cancellation token to cancel the task</param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task JoinAsync(int code, string? username = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disconnects the client from the Kahoot game
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the <see cref="Task"/></param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task LeaveAsync(CancellationToken cancellationToken = default);
}
