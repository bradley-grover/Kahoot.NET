namespace Kahoot.NET.Exceptions;

/// <summary>
/// Occurs when trying to login and the server sends no session found back to the client
/// </summary>
[Serializable]
public class NoSessionFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoSessionFoundException"/>
    /// </summary>
    public NoSessionFoundException() : 
        base("No session was found when trying to login, this could occur because of the game terminating just before you login or another error")
    {

    }
}
