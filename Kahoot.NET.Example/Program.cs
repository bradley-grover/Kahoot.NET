using Kahoot.NET;
using Kahoot.NET.Client;
using Kahoot.NET.Client.Data;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Example;

public class Program
{
    public static async Task Main(string[] args)
    {
        // simple console app
        var factory = LoggerFactory.Create(x => {
            x.AddConsole();
            x.SetMinimumLevel(LogLevel.Debug);
        });

        // has using statement

        using IKahootClient kahootClient = new KahootClient(factory.CreateLogger<IKahootClient>(), new HttpClient());

        int result;

        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine($"Could not get numeric code, try again");
        }

        kahootClient.Joined += KahootClient_OnJoined;
        kahootClient.ClientError += KahootClient_ClientError;
        kahootClient.Left += KahootClient_Left;

       
        var validGame = await kahootClient.JoinAsync(result);

        if (!validGame)
        {
            Console.WriteLine("Could not find game");
            return;
        }

        while (true)
        {
            string? input = Console.ReadLine();

            if (input is not null)
            {
                switch (input)
                {
                    case "leave":
                        await kahootClient.LeaveAsync();
                        break;
                }
            }
        }
    }

    private static Task KahootClient_Left(object? sender, LeftEventArgs args)
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
        }
        return Task.CompletedTask;
    }

    private static Task KahootClient_ClientError(object? sender, ClientErrorEventArgs arg)
    {
        Console.WriteLine(arg.Error);
        return Task.CompletedTask;
    }

    private static Task KahootClient_OnJoined(object? sender, JoinEventArgs args)
    {
        if (args.Success)
        {
            Console.WriteLine("Joined the game");
        }
        else 
        {
            Console.WriteLine("Failed to join game");
        }

        return Task.CompletedTask;
    }
}
