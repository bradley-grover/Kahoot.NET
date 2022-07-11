namespace Kahoot.NET.API;

/// <summary>
/// Connection constants for the websocket
/// </summary>
internal static class Connection
{
    internal const string MinVersion = "1.0";
    internal const string Version = "1.0";
    internal static readonly string[] SupportedConnectionTypes = { "websocket", "long-polling", "callback-polling" };
    internal const string ConnectionType = "websocket";

    internal const string SessionHeader = "x-kahoot-session-token";
    internal const string SessionUrl = "https://kahoot.it/reserve/session/{0}/?{1}";
}
