namespace Kahoot.NET.Internal;

/// <summary>
/// Represents the websocket channels that the bot receives/sends data from
/// </summary>
internal static class LiveMessageChannels
{
    internal const string Handshake = "/meta/handshake";
    internal const string Connection = "/meta/connect";
    internal const string Disconnection = "/meta/disconnect";
    internal const string Service = "/service/controller";
    internal const string Status = "/service/status";
    internal const string Player = "/service/player";
}
