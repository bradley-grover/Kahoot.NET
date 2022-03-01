namespace Kahoot.NET.Internal;

internal static class LiveMessageChannels
{
    internal const string Handshake = "/meta/handshake";
    internal const string Connection = "/meta/connect";
    internal const string Disconnection = "/meta/disconnect";
    internal const string Service = "/service/controller";
    internal const string Status = "/service/status";
    internal const string Player = "/service/player";
}
