using System.Net.WebSockets;
using Kahoot.NET.API.Authentication;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Host;

public class KahootHost : IKahootHost
{
    private readonly HttpClient _httpClient;
    private readonly ClientWebSocket _ws;
    private readonly ILogger<IKahootHost>? _logger;
    private readonly SemaphoreSlim _sendLock;
    private bool _disposedValue;

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
