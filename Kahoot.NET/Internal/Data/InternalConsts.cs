namespace Kahoot.NET.Internal;

/// <summary>
/// Internals consts used for default values during setup and other messages to kahoot
/// </summary>
internal class InternalConsts
{
    internal const string MinVersion = "1.0";
    internal const string Version = "1.0";
    internal static readonly string[] SupportedConnectionTypes = { "websocket", "long-polling", "callback-polling" };
    internal const string ConnectionType = "websocket";
}
