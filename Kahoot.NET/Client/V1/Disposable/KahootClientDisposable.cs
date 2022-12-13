namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private bool disposedValue;

    /// <inheritdoc></inheritdoc>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
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
                if (Socket.State == WebSocketState.Open || Socket.State == WebSocketState.Connecting)
                {
                    Socket.Abort();
                }

                Socket.Dispose();
            }

            disposedValue = true;
        }
    }
}
