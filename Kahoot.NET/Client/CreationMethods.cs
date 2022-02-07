namespace Kahoot.NET.Client;

/*
 * This class contains the static creation methods and constructors
 * The reason the constructor is private is because before giving a library user an IKahootClient the websocket should be connected
 * so that the user can access the properties without them being null
 */

public partial class KahootClient
{
    /// <summary>
    /// Logger for the client, is optional
    /// </summary>
    private ILogger<IKahootClient>? Logger { get; }
    /// <summary>
    /// Logger for the client, is optional and will be replaced with default
    /// </summary>
    private KahootClientConfig Configuration { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="client"></param>
    public KahootClient(ILogger<IKahootClient>? logger, HttpClient client)
    {
        Logger = logger;
        Client = client;
        Configuration = KahootClientConfig.Default;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="client"></param>
    public KahootClient(KahootClientConfig? config, HttpClient client)
    {
        Configuration = config ?? KahootClientConfig.Default;
        Client = client ?? new();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="factory"></param>
    public KahootClient(KahootClientConfig? config, IHttpClientFactory factory)
    {
        Configuration = config ?? KahootClientConfig.Default;
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        Client = factory.CreateClient();
    }
}
