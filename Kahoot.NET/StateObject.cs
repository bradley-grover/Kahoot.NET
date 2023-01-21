using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET;

/// <summary>
/// Internal state object for storing the websocket information
/// </summary>
internal class StateObject
{
    /// <summary>
    /// The size of the buffer to be used to store messages
    /// </summary>
    public const int BufferSize = 1024; // 1 KiB

    /// <summary>
    /// The current message id count
    /// </summary>
    public ulong id;

    /// <summary>
    /// The number of acknowledgements
    /// </summary>
    public ulong ack;

    /// <summary>
    /// The lag of the connection
    /// </summary>
    public long l;

    /// <summary>
    /// The offset of the connection
    /// </summary>
    public long o;

    /// <summary>
    /// The client identifier so the server knows which session we are
    /// </summary>
    public string? clientId;

    /// <summary>
    /// Get the timesync used for certain objects
    /// </summary>
    /// <returns>A timesync used for websocket messages</returns>
    public ExtOnlyTimesync OnlyTime => new()
    {
        Timesync = new()
        {
            L = l,
            O = o
        }
    };

    /// <summary>
    /// Gets extra data from the state object
    /// </summary>
    /// <returns></returns>
    public ExtWithTimesync<long> ExtWithTimesync => new()
    {
        Time = new() { L = l, O = o },
        Acknowledged = (long)ack
    };
}
