namespace Kahoot.NET.API;

/* These channels are mirrored from the index.js when you play a kahoot game */

/// <summary>
/// Class to hold string constants that the Kahoot! API use for their internal websockets
/// </summary>
public static class Channels
{
    /// <summary>
    /// Channel used to handle connecting to the game
    /// </summary>
    public const string Handshake = "/meta/handshake";

    /// <summary>
    /// Channel used for connecting the game as well as hearbeats
    /// </summary>
    public const string Connect = "/meta/connect";

    /// <summary>
    /// Channel used when disconnecting from the websocket/game
    /// </summary>
    public const string Disconnection = "/meta/disconnect";

    /// <summary>
    /// Channel used for login
    /// </summary>
    public const string Service = "/service/controller";

    /// <summary>
    /// Channel used for status updates on the game
    /// </summary>
    public const string Status = "/service/status";

    /// <summary>
    /// Channel used for player updates
    /// </summary>
    public const string Player = "/service/player";
}
