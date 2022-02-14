namespace Kahoot.NET.Client;

/// <summary>
/// Abstraction for a KahootClient
/// </summary>
public interface IKahootClient : IDisposable
{
    /// <summary>
    /// The client connected's id
    /// </summary>
    int? ClientId { get; }
    /// <summary>
    /// Represents the <see cref="GameMode"/> of the <see cref="IKahootClient"/>
    /// </summary>
    GameMode? Mode { get; }
    /// <summary>
    /// Represents the <see cref="INemesis"/> of the <see cref="IKahootClient"/>, 
    /// if they have no nemesis the value will <see langword="null"/>
    /// </summary>
    INemesis? Nemesis { get; }
    /// <summary>
    /// Represents the current <see cref="IQuiz"/> of the <see cref="IKahootClient"/>
    /// </summary>
    /// <remarks>
    /// Would not recommend calling functions from here as this is mainly to see properties
    /// </remarks>
    IQuiz? Quiz { get; }

    /// <inheritdoc></inheritdoc>
    int? SessionId { get; }
    /// <summary>
    /// The total score of the <see cref="IKahootClient"/> in an <see cref="int"/>
    /// </summary>
    int? TotalScore { get; }
    /// <summary>
    /// The username of the <see cref="IKahootClient"/>
    /// </summary>
    string? UserName { get; }

    /// <summary>
    /// Raised when the client connects
    /// </summary>
    event EventHandler? OnJoined;
    /// <summary>
    /// Raised when the client receives a question from the websocket
    /// </summary>
    event EventHandler<QuestionReceivedEventArgs>? OnQuestionReceived;
    /// <summary>
    /// Raised when the client is kicked or disconnected from the question
    /// </summary>
    event EventHandler? OnQuizDisconnect;
    /// <summary>
    /// Raised when the quiz finishes
    /// </summary>
    event EventHandler? OnQuizFinish;
    /// <summary>
    /// Raised when the quiz starts
    /// </summary>
    event EventHandler? OnQuizStart;

    /// <summary>
    /// Method to answer the current question
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    Task AnswerAsync(OneOf<int, string, int[]> id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Method to join the Kahoot
    /// </summary>
    /// <param name="gameCode"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task JoinAsync(int gameCode, string name, CancellationToken cancellationToken = default);
    /// <summary>
    /// Method when invoked leaves the current quiz that the game is in
    /// </summary>
    /// <returns></returns>
    Task LeaveAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Reconnects to the websocket, should be used handling <see cref="OnQuizDisconnect"/>
    /// </summary>
    /// <returns></returns>
    Task ReconnectAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Sends feedback to the game about how the quiz is using <see cref="Feedback"/>
    /// </summary>
    /// <param name="feedback"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendFeedbackAsync(Feedback feedback, CancellationToken cancellationToken = default);
}
