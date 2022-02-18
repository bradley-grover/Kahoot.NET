namespace Kahoot.NET.Exceptions;

/// <summary>
/// Thrown when the client tries to connect to Kahoot but has no game id
/// </summary>
[Serializable]
public class NoGameIdException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoGameIdException"/> class
    /// </summary>
    public NoGameIdException() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NoGameIdException"/> class
    /// </summary>
    /// <param name="message"></param>
    public NoGameIdException(string message) : base(message) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NoGameIdException"/> class
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public NoGameIdException(string message, Exception inner) : base(message, inner) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NoGameIdException"/>
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected NoGameIdException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
