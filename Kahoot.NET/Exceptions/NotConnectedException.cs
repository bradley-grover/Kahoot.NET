namespace Kahoot.NET.Exceptions;

/// <summary>
/// Exception thrown when the client is not connected to any websocket
/// </summary>
[Serializable]
public class NotConnectedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotConnectedException"/>
    /// </summary>
    public NotConnectedException() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NotConnectedException"/>
    /// </summary>
    /// <param name="message">Message of the exception</param>
    public NotConnectedException(string message) : base(message) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NotConnectedException"/>
    /// </summary>
    /// <param name="message">Message of the exception</param>
    /// <param name="inner">Inner exception</param>
    public NotConnectedException(string message, Exception inner) : base(message, inner) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NotConnectedException"/>
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected NotConnectedException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
