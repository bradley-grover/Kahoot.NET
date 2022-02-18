namespace Kahoot.NET.Exceptions;

/// <summary>
/// Thrown when the session request header is unable to be located
/// </summary>

[Serializable]
public class NoSessionHeaderFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoSessionHeaderFoundException"/>
    /// </summary>
    public NoSessionHeaderFoundException() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NoSessionHeaderFoundException"/>
    /// </summary>
    /// <param name="message"></param>
    public NoSessionHeaderFoundException(string message) : base(message) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NoSessionHeaderFoundException"/>
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public NoSessionHeaderFoundException(string message, Exception inner) : base(message, inner) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="NoSessionHeaderFoundException"/>
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected NoSessionHeaderFoundException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
