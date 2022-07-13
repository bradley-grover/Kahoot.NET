using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API;

/// <summary>
/// State object for storing the websocket information
/// </summary>
internal class StateObject
{
    /// <summary>
    /// 1 Kilobyte for reading from the buffer
    /// </summary>
    public const int BufferSize = 1024;

    public long id;
    public long ack;
    public long l;
    public long o;
    public string? clientId;

    internal ExtOnlyTimesync OnlyTimeFromState()
    {
        return new()
        {
            Timesync = new()
            {
                L = l,
                O = o
            }
        };
    }
}
