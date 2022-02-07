[assembly: InternalsVisibleTo("Kahoot.NET.ConsoleDemo")]

namespace Kahoot.NET.Client;

#pragma warning disable CS1998

public partial class KahootClient : IKahootClient
{
    #region Events
    public event EventHandler? OnJoined;
    public event EventHandler<QuestionReceivedEventArgs>? OnQuestionReceived;
    public event EventHandler? OnQuizStart;
    public event EventHandler? OnQuizFinish;
    public event EventHandler? OnQuizDisconnect;

    #endregion

    #region Properties
    /// <summary>
    /// <see cref="HttpClient"/> used for some methods to connect
    /// </summary>
    private HttpClient Client { get; }
    /// <summary>
    /// The Kahoot game id to connect to
    /// </summary>
    internal int? GameId { get; set; }

    /// <inheritdoc></inheritdoc>
    public IQuiz? Quiz { get; private set; }

    /// <inheritdoc></inheritdoc>
    public INemesis? Nemesis { get; private set; }
    /// <inheritdoc></inheritdoc>
    public int? SessionId { get; private set; }
    /// <inheritdoc></inheritdoc>
    public string? UserName { get; private set; }
    /// <inheritdoc></inheritdoc>
    public int? TotalScore { get; private set; }
    /// <inheritdoc></inheritdoc>
    public int? ClientId { get; private set; }
    /// <inheritdoc></inheritdoc>
    public GameMode? Mode { get; private set; }

    #endregion

    #region Methods

    public async Task LeaveAsync(CancellationToken cancellationToken = default)
    {
        if (Socket is null)
        {
            return;
        }
        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, 
            "Client is disconnecting",
            cancellationToken);
    }
    public async Task ReconnectAsync(CancellationToken cancellationToken)
    {

    }
    public async Task SendFeedbackAsync(Feedback feedBack, CancellationToken cancellationToken = default)
    {

    }

    public async Task AnswerAsync(OneOf<int, string, int[]> id, CancellationToken cancellationToken = default)
    {

    }

    public async Task JoinAsync(int gameCode, string name, CancellationToken cancellationToken = default)
    {
        GameId = gameCode;

        Logger?.LogInformation("Received game code attempting to create handshake");

        await CreateHandshakeAsync(cancellationToken);
    }

    #endregion
}
