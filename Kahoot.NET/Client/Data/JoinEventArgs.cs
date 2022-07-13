using System.Diagnostics.CodeAnalysis;
using Kahoot.NET.Client.Data.Errors;

namespace Kahoot.NET.Client.Data;

/// <summary>
/// The result of a user trying to join a game
/// </summary>
public class JoinEventArgs : EventArgs
{
    /// <summary>
    /// If joining the game was a success
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// If <see href="Success"/> is <see langword="false"/> this will have a value
    /// </summary>
    public JoinErrors? Error { get; set; } = null;

    /// <summary>
    /// Unwraps the Error safely so the compiler knows the JoinError is not null
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public bool TryGetError([NotNullWhen(true)] out JoinErrors? error)
    {
        if (Success)
        {
            error = null;
            return false;
        }

        error = Error!.Value;

        return true;
    }
}
