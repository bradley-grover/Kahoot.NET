namespace Kahoot.NET.Exceptions;

/// <summary>
/// Exception thrown when the websockets first response is not successful
/// </summary>
[Serializable]
public class CouldNotEstablishConnectionException  : Exception
{
    /// <summary>
    /// Intializes a new instance of the <see cref="CouldNotEstablishConnectionException"/>
    /// </summary>
    public CouldNotEstablishConnectionException() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="CouldNotEstablishConnectionException"/>
    /// </summary>
    /// <param name="message"></param>
    public CouldNotEstablishConnectionException(string message) : base(message) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="CouldNotEstablishConnectionException"/>
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public CouldNotEstablishConnectionException(string message, Exception inner) : base(message, inner) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="CouldNotEstablishConnectionException"/>
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected CouldNotEstablishConnectionException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
