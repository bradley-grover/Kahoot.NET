using Kahoot.NET.Client.Data;
using Kahoot.NET.Client.Data.Errors;

namespace Kahoot.NET.Example;

public static class ClientEvents
{
    public static Task KahootClient_Left(object? sender, LeftEventArgs args)
    {
        switch (args.Reason)
        {
            case ReasonForLeaving.UserKicked:
                Console.WriteLine("I was kicked from the game");
                break;
            case ReasonForLeaving.UserRequested:
                Console.WriteLine("I have left the game");
                break;
            case ReasonForLeaving.GameLocked:
                Console.WriteLine("The game is locked");
                break;
            case ReasonForLeaving.QueueFull:
                Console.WriteLine("The game has too many players for the client to join");
                break;
        }
        return Task.CompletedTask;
    }

    public static Task KahootClient_ClientError(object? sender, ClientErrorEventArgs arg)
    {
        Console.WriteLine(arg.Error);
        return Task.CompletedTask;
    }

    public static Task KahootClient_OnJoined(object? sender, JoinEventArgs args)
    {
        if (args.Success)
        {
            Console.WriteLine("Joined the game");
            return Task.CompletedTask;
        }

        Console.WriteLine("Failed to join game");

        switch (args.Error)
        {
            case JoinErrors.GameRequires2fa:
                Console.WriteLine("The game requires 2FA to join");
                break;
            case JoinErrors.SessionUnknown:
                Console.WriteLine("The session is unknown");
                break;
            case JoinErrors.DuplicateUserName:
                Console.WriteLine("The username is a duplicate of another");
                break;
            case JoinErrors.Locked:
                Console.WriteLine("The game has locked by the creator");
                break;
        }

        return Task.CompletedTask;
    }
}
