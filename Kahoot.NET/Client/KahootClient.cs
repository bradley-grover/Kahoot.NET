using System.Runtime.CompilerServices;

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

    private HttpClient Client { get; }
    internal int? GameId { get; set; }

    public IQuiz? Quiz { get; private set; }
    public INemesis? Nemesis { get; private set; }
    public int? SessionId { get; private set; }
    public string? UserName { get; private set; }
    public int? TotalScore { get; private set; }
    public int? ClientId { get; private set; }
    public GameMode? Mode { get; private set; }

    #endregion

    #region Methods

    public async Task AnswerAsync(int position)
    {

    }
    public async Task LeaveAsync()
    {

    }
    public async Task ReconnectAsync()
    {

    }
    public async Task SendFeedbackAsync(byte fun, bool learned, bool wouldRecommend, int overall)
    {

    }

    public async Task AnswerAsync(OneOf<int, string, int[]> id)
    {
       
    }

    public async Task JoinAsync(int gameCode, string name)
    {
        GameId = gameCode;
        await CreateHandshakeAsync();
    }

    #endregion
}
