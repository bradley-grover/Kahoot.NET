using System.Net.Http;

namespace Kahoot.NET.Client;

/*
 * This class contains the static creation methods and constructors
 * The reason the constructor is private is because before giving a library user an IKahootClient the websocket should be connected
 * so that the user can access the properties without them being null
 */

public partial class KahootClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="client"></param>
    public KahootClient(HttpClient client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));
        Client = client;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="KahootClient"/> class
    /// </summary>
    /// <param name="factory"></param>
    public KahootClient(IHttpClientFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        Client = factory.CreateClient();
    }
}
