namespace Kahoot.NET.Exceptions;

/// <summary>
/// Thrown when the client cannot find the game that the library user has requested
/// </summary>
[Serializable]
public class GameNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameNotFoundException"/> class
    /// </summary>
    public GameNotFoundException() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="GameNotFoundException"/> class
    /// </summary>
    /// <param name="message">Message of the exception</param>
    public GameNotFoundException(string message) : base(message) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="GameNotFoundException"/> class
    /// </summary>
    /// <param name="message">Message of the exception</param>
    /// <param name="inner">Inner exception of this exception</param>
    public GameNotFoundException(string message, Exception inner) : base(message, inner) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="GameNotFoundException"/> class
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected GameNotFoundException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
