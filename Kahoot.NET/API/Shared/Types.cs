namespace Kahoot.NET.API.Shared;

internal static class Types
{
    public const string Login = "login";
    public const string LoginResponse = "loginResponse";
    public const string Message = "message";
    public const string Status = "status";

    internal static class Errors
    {
        public const string UserInput = "USER_INPUT";
        public const string Locked = "LOCKED";
    }
}
