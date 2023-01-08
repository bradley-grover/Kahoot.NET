using System.IO;
using System.Runtime.InteropServices;
using Kahoot.NET.API.Authentication;
using Kahoot.NET.Client;
using Kahoot.NET.Client.Events;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Kahoot.NET.CLI;

public class Program
{
    internal static readonly HttpClient _client = new();
    internal static ILoggerFactory _loggerFactory = LoggerFactory.Create(b =>
    {
        b.ClearProviders();
    });

    public static async Task Main()
    {
        Console.Title = $"Kahoot.NET.CLI - {RuntimeInformation.FrameworkDescription}";

        AnsiConsole.Write(
            new FigletText("Kahoot.NET.CLI")
            .Centered()
            .Color(Color.Green));

        AnsiConsole.WriteLine();

        while (true)
        {
            await RunAsync();

            if (!AnsiConsole.Confirm("Run another time?", false))
            {
                break;
            }
        }
    }

    internal static async Task RunAsync()
    {
        int gameCode = AnsiConsole.Prompt(new TextPrompt<int>("Enter a Kahoot game code:\n")
            .PromptStyle("green"));


        var runMode = AnsiConsole.Prompt(new SelectionPrompt<RunMode>()
            .Title("Preference on number of clients:\n")
            .AddChoices(RunMode.Singular, RunMode.Multiple));

        switch (runMode)
        {
            case RunMode.Singular:
                await SingleClientAsync(gameCode);
                break;
        }
    }

    internal static async Task SingleClientAsync(int gameCode)
    {
        string username = AnsiConsole.Prompt(new TextPrompt<string>("Enter a username for this one client:\n")
            .PromptStyle("green"));

        using IKahootClient client = new KahootClient(logger: _loggerFactory.CreateLogger<IKahootClient>(), httpClient: _client);

        client.Joined += Client_Joined;
        client.Left += Client_Left;

        if (!await client.JoinAsync(gameCode, username))
        {
            AnsiConsole.Write(new Text("There was an error joining the game", new Style(foreground: Color.Red)));
            AnsiConsole.WriteLine();

            return;
        }

        while (client.IsConnected)
        {
            await Task.Delay(1000);
        }
    }

    private static Task Client_Left(object? sender, LeftEventArgs leftArgs)
    {
        string message;

        switch (leftArgs.Condition)
        {
            case LeaveCondition.Locked:
                message = "The game is locked so I have left";
                break;
            case LeaveCondition.Kicked:
                message = "I was kicked from the game";
                break;
            case LeaveCondition.JoinFailure:
                message = "There was a failure joining the game, so the client has left";
                break;
            case LeaveCondition.Requested:
                message = "I left the game";
                break;
            case LeaveCondition.Full:
                message = "The game is full, which likely means that it has reached the 2000 game limit";
                break;
            default:
                message = "Unknown reason for leaving";
                break;
        }

        AnsiConsole.Write(new Text(message, new Style(foreground: Color.Red)));
        AnsiConsole.WriteLine();

        return Task.CompletedTask;
    }

    private static Task Client_Joined(object? sender, JoinEventArgs joinArgs)
    {
        string? message = null;
        Color color;

        if (joinArgs.IsSuccess)
        {
            message = $"Joined the game with the pin {joinArgs.Code}";
            color = Color.Green;
        }
        else
        {
            color = Color.Red;
        }

        switch (joinArgs.Result)
        {
            case JoinResult.DuplicateUserName:
                message = "Could not join the game because somebody with the same username has already joined the game";
                break;
            case JoinResult.SessionUnknown:
                message = "Could not join the game because the client could not find the session, " +
                    "this might have happened because the client was joining the game whilst the game stopped";
                break;
            case JoinResult.Locked:
                message = "Could not join the game because the host has locked the game from anyone joining";
                break;
            case JoinResult.GameRequires2fa:
                message = "Could not join the game because the game requires 2fa in the join process";
                break;
        }

        AnsiConsole.Write(new Text(message!, new Style(foreground: color)));
        AnsiConsole.WriteLine();

        return Task.CompletedTask;
    }
}
