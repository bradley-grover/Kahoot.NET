namespace Kahoot.NET.Client;

public partial class KahootClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="logger">A logger for the client</param>
    public KahootClient(ILogger<IKahootClient>? logger = null)
    {
        Logger = logger;
        Client = new();
        Socket = Session.GetConfiguredWebSocket();
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
        Socket = Session.GetConfiguredWebSocket();
    }

    /// <summary>
    /// Initializes a new instance of the <seealso cref="KahootClient"/> class
    /// </summary>
    /// <param name="logger">A logger for the client</param>
    /// <param name="httpClientFactory">An factory to produce <seealso cref="HttpClient"/> so that we don't starve our threads</param>
    public KahootClient(ILogger<IKahootClient> logger, IHttpClientFactory httpClientFactory)
    {
        Logger = logger;
        Client = httpClientFactory.CreateClient();
        Socket = Session.GetConfiguredWebSocket();
    }
}
