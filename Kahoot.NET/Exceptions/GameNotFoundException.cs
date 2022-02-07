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
    public GameNotFoundException(string message) : base(message) { }
    public GameNotFoundException(string message, Exception inner) : base(message, inner) { }
    protected GameNotFoundException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
