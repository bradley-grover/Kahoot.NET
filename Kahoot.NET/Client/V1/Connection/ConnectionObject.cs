namespace Kahoot.NET.Client;

/// <summary>
/// Represents the connection object, this is used for the websocket
/// </summary>
/// <remarks>
/// <para>
/// We can not use properties instead of fields here as properties can not be passed by the <see langword="ref"/> keyword in the <see cref="Interlocked"/> class
/// </para>
/// </remarks>
internal class ConnectionObject
{
    public long id;
    public long ack;
    public long l;
    public long o;
    public string? clientId;
}
