namespace Kahoot.NET.Client;

/// <summary>
/// Client to connect to a Kahoot
/// </summary>
public interface IKahootClient : IDisposable
{
    /// <summary>
    /// Join a kahoot game with the name and code
    /// </summary>
    /// <param name="code">The code of the game</param>
    /// <param name="username">The username of the user to join the game</param>
    /// <param name="cancellationToken">A cancellation token to cancel the task</param>
    /// <returns></returns>
    Task JoinAsync(int code, string? username = null, CancellationToken cancellationToken = default);
}
