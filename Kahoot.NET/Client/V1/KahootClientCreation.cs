namespace Kahoot.NET.Client;

public partial class KahootClient
{
    /// <summary>
    /// Initilizes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    public KahootClient()
    {
        Client = new();
        Socket = new();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="logger">A logger for the client</param>
    public KahootClient(ILogger<IKahootClient>? logger)
    {
        Logger = logger;
        Client = new();
        Socket = new();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="logger">A logger for the client</param>
    /// <param name="client">A http client to be used to message kahoot</param>
    public KahootClient(ILogger<IKahootClient> logger, HttpClient client)
    {
        Logger = logger;
        Client = client;
        Socket = new();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="logger">A logger for the client</param>
    /// <param name="httpClientFactory">An factory to produce <see cref="HttpClient"/> so that we don't starve our threads</param>
    public KahootClient(ILogger<IKahootClient> logger, IHttpClientFactory httpClientFactory)
    {
        Logger = logger;
        Client = httpClientFactory.CreateClient();
        Socket = new();
    }
}
