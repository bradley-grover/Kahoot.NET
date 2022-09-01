using Kahoot.NET.API;
using Kahoot.NET.Client.Data;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private ClientWebSocket Socket { get; set; }
    private HttpClient Client { get; }
    private ILogger<IKahootClient>? Logger { get; }

    private StateObject State { get; } = new()
    {
        id = 1,
        ack = 0,
        l = 68,
        o = 2999
    };

    internal static JsonSerializerOptions SerializerOptions { get; } = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    internal int GameId { get; set; }
    /// <summary>
    /// Username of the current client
    /// </summary>
    public string? Username { get; private set; }

    /// <inheritdoc></inheritdoc>
    public event Func<object?, JoinEventArgs, Task>? Joined;

    /// <inheritdoc></inheritdoc>
    public event Func<object?, ClientErrorEventArgs, Task>? ClientError;

    /// <inheritdoc></inheritdoc>
    public event Func<object?, LeftEventArgs, Task>? Left;
}
