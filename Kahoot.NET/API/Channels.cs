namespace Kahoot.NET.API;

/// <summary>
/// Class to hold string constants that the Kahoot! API use for their internal websockets
/// </summary>
public static class Channels
{
    /// <summary>
    /// Channel used to handle connecting to the game
    /// </summary>
    internal const string Handshake = "/meta/handshake";
    internal const string Connection = "/meta/connect";
    /// <summary>
    /// Channel used when disconnecting from the websocket/game
    /// </summary>
    internal const string Disconnection = "/meta/disconnect";
    internal const string Service = "/service/controller";
    internal const string Status = "/service/status";
    internal const string Player = "/service/player";
}
