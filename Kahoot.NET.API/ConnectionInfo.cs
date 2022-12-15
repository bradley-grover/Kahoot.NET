namespace Kahoot.NET.API;

/// <summary>
/// Connection constants that the websocket uses
/// </summary>
public static class ConnectionInfo
{
    /// <summary>
    /// The minimum version of the connection
    /// </summary>
    public const string MinVersion = "1.0";

    /// <summary>
    /// The version of the connection
    /// </summary>
    public const string Version = "1.0";

    /// <summary>
    /// Supported connection types that the websocket accepts
    /// </summary>
    public static IReadOnlyCollection<string> SupportedConnectionTypes => Array.AsReadOnly(_supportedConnectionTypes);

    private static readonly string[] _supportedConnectionTypes = new string[] { "websocket", "long-polling", "callback-polling" };

    /// <summary>
    /// The connection type for the game
    /// </summary>
    public const string ConnectionType = "websocket";

    /// <summary>
    /// The name of the header which contains the token used for decoding to connect to the websocket
    /// </summary>
    public const string SessionHeader = "x-kahoot-session-token";

    /// <summary>
    /// Represents the url to create a session and get the challenge function and header to connect to the websocket
    /// </summary>
    public const string SessionUrl = "https://kahoot.it/reserve/session/{0}/?{1}";

    /// <summary>
    /// Represents the url used to create a session to host a game
    /// </summary>
    public const string HostSessionUrl = "https://play.kahoot.it/reserve/session/?{0}";

    /// <summary>
    /// The websocket url for hosting
    /// </summary>
    public const string HostWebsocketUrl = "wss://play.kahoot.it/cometd/{0}/{1}";

    /// <summary>
    /// The websocket url to connect to the game
    /// </summary>
    public const string WebsocketUrl = "wss://kahoot.it/cometd/{0}/{1}";

    /// <summary>
    /// <see cref="WebsocketUrl"/> with no formatting
    /// </summary>
    public const string WebSocketUrlNoFormat = "wss://kahoot.it/cometd/";

    /// <summary>
    /// Host used in some requests and websocket events
    /// </summary>
    public const string Host = "kahoot.it";
}
