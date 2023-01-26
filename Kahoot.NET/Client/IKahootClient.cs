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
    /// The current username of the client, it is null when <see cref="IsConnected"/> is false
    /// </summary>
    /// <remarks>
    /// The recommended way to check whether this null is by using <see cref="IsConnected"/> like:
    /// <example>
    /// <code>
    /// if (client.IsConnected)
    /// {
    ///    string username = client.Username; // not null
    /// }
    /// string? user = client.Username; // needs '?' to not rise warning without <see cref="IsConnected"/> check
    /// </code>
    /// </example>
    /// </remarks>
    [MemberNotNullWhen(true, nameof(IsConnected))]
    string? Username { get; }

    /// <summary>
    /// The code that the client is currently in, use <see cref="IsConnected"/> to check whether this is null
    /// </summary>
    /// <remarks>
    /// The recommended way to check whether this null is by using <see cref="IsConnected"/> like:
    /// <example>
    /// <code>
    /// if (client.IsConnected)
    /// {
    ///    uint code = client.Code; // not null
    /// }
    /// uint? nullCode = client.Code; // needs '?' to not rise warning without <see cref="IsConnected"/> check
    /// </code>
    /// </example>
    /// </remarks>
    [MemberNotNullWhen(true, nameof(IsConnected))]
    uint? Code { get; }

    /// <summary>
    /// Event triggered when the <see cref="JoinAsync(uint, string, CancellationToken)"/> action completes its task
    /// </summary>
    event Func<object?, JoinEventArgs, Task> Joined;

    /// <summary>
    /// Event triggered when the client leaves the game
    /// </summary>
    event Func<object?, LeftEventArgs, Task> Left;

    /// <summary>
    /// Event triggered when the host starts the quiz
    /// </summary>

    event Func<object?, QuizStartedEventArgs, Task> QuizStarted;

    /// <summary>
    /// Event triggered when the client receives the question
    /// </summary>
    event Func<object?, QuestionReceivedArgs, Task> QuestionReceived;

    /// <summary>
    /// Event triggered when the host wants feedback from the players
    /// </summary>
    event Func<object?, EventArgs, Task> FeedbackRequested;

    /// <summary>
    /// Sends a question answer to the host if the question is answerable, else it ignores it.
    /// </summary>
    /// <param name="quizQuestion"></param>
    /// <param name="answerIndex"></param>
    /// <param name="array"></param>
    /// <param name="text"></param>
    /// <remarks>
    /// Should be used in unision with <see cref="QuestionReceived"/> or else the host will get confused.
    /// For which param would be used to answer you should perform a check of <see cref="QuizQuestionData.QuestionType"/> and answer accordingly.
    /// If the answer type is incorrect the method ignores
    /// </remarks>
    ValueTask AnswerAsync(QuizQuestionData quizQuestion, int? answerIndex = null, int[]? array = null, string? text = null);

    /// <summary>
    /// The client begins to join the game and will report its results to the delegate property
    /// </summary>
    /// <param name="code"></param>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Whether the client has completed the handshake process without error. **This doesn't mean that the client has joined the game yet</returns>
    /// <remarks>
    /// The result of this operation will received at <see cref="QuestionReceived"/>
    /// </remarks>
    Task<bool> JoinAsync(uint code, string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Use this function when <see cref="FeedbackRequested"/> is triggered, and want to send game feedback to the host
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    ValueTask SendFeedbackAsync(Feedback feedback);

    /// <summary>
    /// Disconnects the client from the Kahoot! game
    /// </summary>
    /// <remarks>
    /// This will also trigger <see cref="Left"/> event
    /// </remarks>
    Task LeaveAsync();
}
