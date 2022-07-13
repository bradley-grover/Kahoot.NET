namespace Kahoot.NET.API.Shared;

/// <summary>
/// The types that can be included in the <see cref="Data.Type"/> field
/// </summary>
public static class Types
{
    /// <summary>
    /// Data type used for when the user is logging in/authenticating to the game
    /// </summary>
    public const string Login = "login";

    /// <summary>
    /// The response received when the login info is received, like the name
    /// </summary>
    public const string LoginResponse = "loginResponse";

    /// <summary>
    /// Represents a general message for various data types
    /// </summary>
    public const string Message = "message";

    /// <summary>
    /// Status updates about certain events
    /// </summary>
    public const string Status = "status";

    /// <summary>
    /// Represents errors that have occured contained within the data
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// Represents when the user input is bad, like a duplicate user name
        /// </summary>
        public const string UserInput = "USER_INPUT";

        /// <summary>
        /// When the user tries to authenticate to a locked quiz
        /// </summary>
        public const string Locked = "LOCKED";
    }
}
