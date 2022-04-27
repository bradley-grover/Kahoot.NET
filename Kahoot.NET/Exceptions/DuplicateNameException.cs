namespace Kahoot.NET.Exceptions;

/// <summary>
/// Exception thrown when you enter a name that is already in the specified Kahoot!
/// </summary>
[Serializable]
public class DuplicateNameException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateNameException"/> class
    /// </summary>
    public DuplicateNameException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateNameException"/> class
    /// </summary>
    /// <param name="name">The name that caused the error</param>
    public DuplicateNameException(string name) : base($"Duplicate name: cannot join game with name of {name}") { }
}
