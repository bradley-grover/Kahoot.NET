namespace Kahoot.NET.Client;

/// <summary>
/// The current client used to interact and join Kahoot! games with
/// </summary>
public partial class KahootClient : IKahootClient
{
    // static
    internal static readonly JsonSerializerOptions _serializerOptions = new() // use these serialize options when source generation is not possible
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // readonly fields
    private readonly HttpClient _httpClient;
    private readonly ClientWebSocket _ws;
    private readonly ILogger<IKahootClient>? _logger;
    private readonly string _userAgent;
    private readonly SemaphoreSlim _senderLock;

    // mutable
    private string? _username;
    private uint _code;
    private bool _usingNamerator;
    private bool _disposedValue;

    internal readonly StateObject _stateObject = new() // TODO: Investigate how to obtain proper values for this class object
    {
        id = 1,
        ack = 0,
        l = 68,
        o = 2999
    };

    /// <summary>
    /// The integer game code of the currently in game, 0 is none
    /// </summary>
    public uint? Code
    {
        get => _code;
    }

    /// <summary>
    /// The current username of the client, if any
    /// </summary>
    public string? Username
    {
        get => _username;
    }

    /// <summary>
    /// Checks whether the client is in a game
    /// </summary>
    public bool IsConnected
    {
        get => _ws.State == WebSocketState.Open;
    }

    /// <inheritdoc/>
    public event Func<object?, JoinEventArgs, Task> Joined;

    /// <inheritdoc/>
    public event Func<object?, LeftEventArgs, Task> Left;

    /// <inheritdoc/>
    public event Func<object?, QuestionReceivedArgs, Task> QuestionReceived;

    /// <inheritdoc/>
    public event Func<object?, QuizStartedEventArgs, Task> QuizStarted;

    /// <inheritdoc/>
    public event Func<object?, EventArgs, Task> FeedbackRequested;

    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class with an optional logger or/and HttpClient
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpClient"></param>
    /// <param name="userAgent"></param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public KahootClient(ILogger<IKahootClient>? logger = null, HttpClient? httpClient = null, string? userAgent = null)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _logger = logger;
        _httpClient = httpClient ?? new HttpClient(); // create new as fall back
        _userAgent = userAgent ?? UserAgent.Generate(); // if the caller doesn't provide an agent generate our own
        _ws = Session.GetConfiguredWebSocket(StateObject.BufferSize, StateObject.BufferSize); // use default size for websocket
        _ws.Options.SetRequestHeader("User-Agent", _userAgent); // set the user agent for the request
        _senderLock = new SemaphoreSlim(1);
    }


    /// <inheritdoc/>
    public async Task<bool> JoinAsync(uint code, string username, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));

        if (_ws.State == WebSocketState.Open) throw new InvalidOperationException("The client is already in another game");

        await _senderLock.WaitAsync(cancellationToken).ConfigureAwait(false);

        _username = username;
        _code = code;

        _logger?.LogDebug("Trying to join game");

        _senderLock.Release();

        if (!await TryConnectAsync(cancellationToken))
        {
            Debug.WriteLine("Failed to connect");
            return false;
        }

        _ = Task.Run(ReceiveAsync, cancellationToken);

        return true;
    }

    /// <inheritdoc/>
    public async ValueTask AnswerAsync(QuizQuestionData quizQuestionData, int? questionIndex = null, int[]? array = null, string? text = null)
    {
        if (quizQuestionData is null)
        {
            return;
        }

        var quizAnswerContent = new QuestionAnswerContent
        {
            QuestionIndex = quizQuestionData.QuestionIndex,
            Type = quizQuestionData.Type
        };

        switch (quizQuestionData.QuestionType)
        {
            case QuestionType.Quiz:
            case QuestionType.Survey:
                if (questionIndex is null) return;
                quizAnswerContent.Choice = questionIndex;
                break;
            case QuestionType.OpenEnded:
            case QuestionType.WordCloud:
                if (text is null) return;
                quizAnswerContent.Choice = text;
                break;

            // all use int[]
            case QuestionType.Jumble:
            case QuestionType.MultipleSelectPoll:
            case QuestionType.MultipleSelect:
                if (array is null || array.Length == 0) return;
                quizAnswerContent.Choice = array;
                break;

            case QuestionType.Content: return; // there is no reason to answer a content question
        }

        var questionAnswer = new QuestionAnswer(quizAnswerContent, _code)
        {
            Id = Interlocked.Read(ref _stateObject.id).ToString(),
            ClientId = _stateObject.clientId,
        };

        await SendAsync(questionAnswer, QuestionAnswerContext.Default.QuestionAnswer).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public Task LeaveAsync() => LeaveAsync(LeaveCondition.Requested);

    // internal leave command used to leave under a range of conditions
    internal async Task LeaveAsync(LeaveCondition condition)
    {
        if (_ws.State != WebSocketState.Open) return;

        await _senderLock.WaitAsync().ConfigureAwait(false);

        try
        {
            _username = default;
            _code = default;

            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, default, default);
            await Left.InvokeEventAsync(this, new(condition));
        }
        finally
        {
            _senderLock.Release();
        }

    }

    /// <inheritdoc/>
    public async ValueTask SendFeedbackAsync(Feedback feedback)
    {
        ArgumentNullException.ThrowIfNull(feedback); // there is no point in answering this if this is null

        feedback.Nickname = _username;

        var request = new FeedbackMessage(feedback, _code)
        {
            Id = Interlocked.Read(ref _stateObject.id).ToString(),
            ClientId = _stateObject.clientId
        };

        await SendAsync(request, FeedbackContext.Default.FeedbackMessage).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _ws.Abort(); // also disposes
            }

            _disposedValue = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
