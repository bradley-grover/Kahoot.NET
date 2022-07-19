using Kahoot.NET.Hosting.Client.Errors;
using Kahoot.NET.Hosting.Resolver;

namespace Kahoot.NET.Hosting.Client;

/// <summary>
/// Standard implementation to host Kahoots
/// </summary>
public partial class KahootHost : IKahootHost
{
    private StateObject State { get; } = new();
    private ClientWebSocket Socket { get; } = new();
    private int _code = default;


    /// <inheritdoc></inheritdoc>
    public int? GameCode
    {
        get
        {
            if (_code == default)
            {
                return null;
            }

            return _code;
        }
    }
    
    private ILogger<IKahootHost>? Logger { get; }
    private HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="KahootHost"/> class with the specified logger
    /// </summary>
    /// <param name="logger">Logger to log events</param>
    public KahootHost(ILogger<IKahootHost>? logger = null)
    {
        Logger = logger;
        httpClient = new HttpClient();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KahootHost"/> class with the specified logger and http client
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="client"></param>
    public KahootHost(ILogger<IKahootHost>? logger, HttpClient client)
    {
        Logger = logger;
        httpClient = client;
    }

    /// <inheritdoc></inheritdoc>
    public event Func<object?, EventArgs, Task>? Created;

    /// <inheritdoc></inheritdoc>
    public async Task<int> CreateGameAsync(Uri quizUrl, GameConfiguration? configuration = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(quizUrl);

        configuration ??= GameConfiguration.Default;

        (int code, string key, bool success) = await httpClient.SendHostRequestAsync(quizUrl, configuration);

        if (!success || !quizUrl.Host.Equals("play.kahoot.it", StringComparison.InvariantCultureIgnoreCase))
        {
            throw new QuizNotFoundException($"The quiz with the url '{quizUrl}' was not found");
        }

        await SendHandshakeAsync(code, key);

        var thread = new Thread(async () => await ReceiveAsync())
        {
            IsBackground = true
        };

        thread.Start();

        return code;
    }

    /// <inheritdoc></inheritdoc>
    public Task LockAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc></inheritdoc>
    public Task StartAsync()
    {
        throw new NotImplementedException();
    }

    #region IDisposable
    private bool disposedValue;
    /// <inheritdoc></inheritdoc>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~KahootHost()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    /// <inheritdoc></inheritdoc>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
