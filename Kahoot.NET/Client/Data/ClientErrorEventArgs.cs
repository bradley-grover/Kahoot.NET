namespace Kahoot.NET.Client.Data;

/// <summary>
/// Events args for when an internal error is thrown
/// </summary>
public class ClientErrorEventArgs : EventArgs
{
    /// <summary>
    /// The error that occured internally
    /// </summary>
    public Exception Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientErrorEventArgs"/>
    /// </summary>
    /// <param name="exception"></param>
    public ClientErrorEventArgs(Exception exception)
    {
        Error = exception;
    }
}
