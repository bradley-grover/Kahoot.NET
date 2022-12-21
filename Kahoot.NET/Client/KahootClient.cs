namespace Kahoot.NET.Client;

/// <summary>
/// The current client used to interact and join Kahoot! games with
/// </summary>
public partial class KahootClient : IKahootClient
{
    // static
    internal static readonly JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // readonly fields
    private readonly HttpClient _httpClient;
    private readonly ClientWebSocket _socket;
    private readonly ILogger<IKahootClient>? _logger;
    private readonly string _userAgent;

    // mutable
    private string? _userName;
    private int _gameCode;
    private bool _usingNamerator;

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
    public int GameCode
    {
        get => _gameCode;
    }

    /// <summary>
    /// The current username of the client, if any
    /// </summary>
    public string? Username
    {
        get => _userName;
    }

    /// <summary>
    /// Checks whether the client is in a game
    /// </summary>
    public bool IsConnected
    {
        get => _socket.State == WebSocketState.Open;
    }

    /// <inheritdoc/>
    public event Func<object?, JoinEventArgs, Task> Joined;

    /// <inheritdoc/>
    public event Func<object?, LeftEventArgs, Task> Left;

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
        _userAgent = userAgent ?? RandomUserAgent.RandomUa.RandomUserAgent; 
        _socket = Session.GetConfiguredWebSocket(StateObject.BufferSize, StateObject.BufferSize);
        _socket.Options.SetRequestHeader("User-Agent", _userAgent);
    }


    /// <inheritdoc/>
    public async Task<bool> JoinAsync(int gameCode, string username, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));

        if (_socket.State == WebSocketState.Open) throw new InvalidOperationException("The client is already in another game");

        _userName = username;
        _gameCode = gameCode;

        _logger?.LogDebug("Trying to join game");

        if (!await TryConnectAsync(cancellationToken))
        {
            return false;
        }

        // TODO: Find better alternative than using old threading approach

        var thread = new Thread(async () => await ReceiveAsync())
        {
            IsBackground = true,
        };

        thread.Start();

        return true;
    }

    /// <inheritdoc/>
    public async Task LeaveAsync()
    {
        if (_socket.State != WebSocketState.Open) return;

        // TODO: Implement leave logic, is there really a point to sending a leave message?

        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, default, default);
    }

    private bool _disposedValue;

    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                if (_socket.State == WebSocketState.Open || _socket.State == WebSocketState.Connecting)
                {
                    _socket.Abort();
                }

                _socket.Dispose();
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
