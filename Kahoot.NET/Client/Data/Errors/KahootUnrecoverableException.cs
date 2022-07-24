namespace Kahoot.NET.Client.Data.Errors;

/// <summary>
/// An exception occurs internally that can't be recovered for the websocket to continue
/// </summary>
[Serializable]
public class KahootUnrecoverableException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KahootUnrecoverableException"/> class
    /// </summary>
    /// <param name="message">The message to display for debugging</param>
    public KahootUnrecoverableException(string message) : base(message) { }
}
