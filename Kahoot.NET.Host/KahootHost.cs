using System.Net.WebSockets;
using System.Text.Json.Serialization;
using System.Text.Json;
using Kahoot.NET.API.Authentication;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Kahoot.NET.Host;

public partial class KahootHost : IKahootHost
{
    // static
    internal static readonly JsonSerializerOptions _serializerOptions = new() // use these serialize options when source generation is not possible
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private readonly HttpClient _httpClient;
    private readonly ClientWebSocket _ws;
    private readonly ILogger<IKahootHost>? _logger;
    private readonly SemaphoreSlim _sendLock;
    private bool _disposedValue;

    internal readonly StateObject _stateObject = new() // TODO: Investigate how to obtain proper values for this class object
    {
        id = 1,
        ack = 0,
        l = 68,
        o = 2999
    };

    public bool IsConnected => _ws.State == WebSocketState.Open;

    public Quiz? Game { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KahootHost"/> class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpClient"></param>
    public KahootHost(ILogger<IKahootHost>? logger = null, HttpClient? httpClient = null)
    {
        _logger = logger;
        _httpClient = httpClient ?? new HttpClient(); // use HttpClient.Shared if added
        _ws = Session.GetConfiguredWebSocket(1024, 1024);
        _sendLock = new(1); // only 1 send operation is allowed at a time
    }

    public async Task<bool> CreateAsync(Uri uri, GameConfiguration? config = default, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(uri);

        config ??= GameConfiguration.Default;

        var result = await SendHostRequestAsync(_httpClient, uri, config);

        if (!result.IsValid)
        {
            return false;
        }

        await SendHandshakeAsync(result.Code, result.Key);

        _ = Task.Run(ReceiveAsync, CancellationToken.None);

        return true;
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _ws.Abort();
                _sendLock.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~KahootHost()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
