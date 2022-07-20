using Kahoot.NET.Client.Data;
using Kahoot.NET.Hosting.Client.Errors;

namespace Kahoot.NET.Hosting.Client;

/// <summary>
/// Host's a Kahoot
/// </summary>
public interface IKahootHost : IDisposable
{
    /// <summary>
    /// The game code of the quiz, will be null if <see cref="CreateGameAsync(Uri, GameConfiguration?, CancellationToken)"/> has not been called
    /// </summary>
    public int? GameCode { get; }

    /// <summary>
    /// Event is raised when <see cref="CreateGameAsync(Uri, GameConfiguration?, CancellationToken)"/> has created a game
    /// </summary>
    event Func<object?, EventArgs, Task>? Created;

    /// <summary>
    /// When the client encounters an error
    /// </summary>
    event Func<object?, ClientErrorEventArgs, Task>? HostError;

    /// <summary>
    /// Creates a game to the specified quiz url
    /// </summary>
    /// <param name="quizUrl"></param>
    /// <param name="configuration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The game code for clients to connect to</returns>
    /// <exception cref="ArgumentNullException">Thrown if the url is null</exception>
    /// <exception cref="QuizNotFoundException">Thrown if the quiz is not found</exception>
    Task<int> CreateGameAsync(Uri quizUrl, GameConfiguration? configuration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts the game with the question
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task StartAsync();

    /// <summary>
    /// Lock's the Kahoot to prevent more players from joining
    /// </summary>
    /// <returns>Awaitable</returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task LockAsync();

    /// <summary>
    /// The host leaves the current game
    /// </summary>
    /// <returns>Task to await</returns>
    Task LeaveAsync();
}
