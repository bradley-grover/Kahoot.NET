namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private ClientWebSocket? Socket { get; set; }
    private HttpClient Client { get; }
    private ILogger<IKahootClient>? Logger { get; }
#nullable disable
    private ConnectionObject _sessionObject;
#nullable restore

    #region Stored
    private CreateSessionResponse? CreateResponse { get; set; }
    #endregion

    internal int? GameId { get; set; }
    /// <summary>
    /// Username of the current client
    /// </summary>
    public string? Username { get; private set; }
}
