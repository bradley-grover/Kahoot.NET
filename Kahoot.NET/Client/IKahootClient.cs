namespace Kahoot.NET.Client;

/// <summary>
/// Abstraction for a KahootClient
/// </summary>
public interface IKahootClient
{
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
    int? SessionId { get; }
    /// <summary>
    /// The total score of the <see cref="IKahootClient"/> in an <see cref="int"/>
    /// </summary>
    int? TotalScore { get; }
    /// <summary>
    /// The username of the <see cref="IKahootClient"/>
    /// </summary>
    string? UserName { get; }

    event EventHandler? OnJoined;
    event EventHandler<QuestionReceivedEventArgs>? OnQuestionReceived;
    event EventHandler? OnQuizDisconnect;
    event EventHandler? OnQuizFinish;
    event EventHandler? OnQuizStart;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    Task AnswerAsync(OneOf<int, string, int[]> id);
    Task JoinAsync(int gameCode, string name);
    Task LeaveAsync();
    Task ReconnectAsync();
    Task SendFeedbackAsync(byte fun, bool learned, bool wouldRecommend, int overall);
}
