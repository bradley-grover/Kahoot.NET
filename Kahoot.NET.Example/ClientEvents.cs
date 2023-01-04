using System.Diagnostics;
using Kahoot.NET.API.Shared;
using Kahoot.NET.Client;
using Kahoot.NET.Client.Events;

namespace Kahoot.NET.Example;

public static class ClientEvents
{
    public static Task KahootClient_Left(object? sender, LeftEventArgs args)
    {
        switch (args.Condition)
        {
            case LeaveCondition.Kicked:
                Console.WriteLine("I was kicked from the game");
                break;
            case LeaveCondition.Requested:
                Console.WriteLine("I have left the game");
                break;
            case LeaveCondition.Locked:
                Console.WriteLine("The game is locked");
                break;
            case LeaveCondition.Full:
                Console.WriteLine("The game has too many players for the client to join");
                break;
        }
        return Task.CompletedTask;
    }

    public static async Task KahootClient_QuestionRec(object? sender, QuestionReceivedArgs args)
    {
        if (args.ShouldIgnore)
        {
            return;
        }

        if (sender is not IKahootClient client)
        {
            return;
        }

        if (args.Question.Type == Types.Question.Quiz)
        {
            await client.RespondAsync(args.Question, Random.Shared.Next(args.Question.NumberOfChoices)); // pick a random answer
        }
    }

    public static Task KahootClient_OnJoined(object? sender, JoinEventArgs args)
    {
        if (args.IsSuccess)
        {
            Console.WriteLine("Joined the game");
            return Task.CompletedTask;
        }

        Console.WriteLine("Failed to join game");

        switch (args.Result)
        {
            case JoinResult.GameRequires2fa:
                Console.WriteLine("The game requires 2FA to join");
                break;
            case JoinResult.SessionUnknown:
                Console.WriteLine("The session is unknown");
                break;
            case JoinResult.DuplicateUserName:
                Console.WriteLine("The username is a duplicate of another");
                break;
            case JoinResult.Locked:
                Console.WriteLine("The game has locked by the creator");
                break;
        }

        return Task.CompletedTask;
    }
}
