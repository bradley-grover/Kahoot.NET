namespace Kahoot.NET.Client;

/// <inheritdoc></inheritdoc>
public partial class KahootClient : IDisposable
{
    /// <summary>
    /// Disposes the Client
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                WebSocket?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            disposedValue = true;
        }
    }
    /// <inheritdoc></inheritdoc>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
