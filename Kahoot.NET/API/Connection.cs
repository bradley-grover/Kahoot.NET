namespace Kahoot.NET.API;

/// <summary>
/// Connection constants that the websocket uses
/// </summary>
internal static class Connection
{
    /// <summary>
    /// The minimum version of the connection
    /// </summary>
    internal const string MinVersion = "1.0";

    /// <summary>
    /// The version of the connection
    /// </summary>
    internal const string Version = "1.0";

    /// <summary>
    /// Supported connection types that the websocket accepts
    /// </summary>
    internal static readonly string[] SupportedConnectionTypes = { "websocket", "long-polling", "callback-polling" };

    /// <summary>
    /// The connection type for the game
    /// </summary>
    internal const string ConnectionType = "websocket";

    /// <summary>
    /// The name of the header which contains the token used for decoding to connect to the websocket
    /// </summary>
    internal const string SessionHeader = "x-kahoot-session-token";

    /// <summary>
    /// Represents the url to create a session and get the challenge function and header to connect to the websocket
    /// </summary>
    internal const string SessionUrl = "https://kahoot.it/reserve/session/{0}/?{1}";

    /// <summary>
    /// The websocket url to connect to the game
    /// </summary>
    internal const string WebsocketUrl = "wss://kahoot.it/cometd/{0}/{1}";

    /// <summary>
    /// Host used in some requests and websocket events
    /// </summary>
    internal const string Host = "kahoot.it";
}
