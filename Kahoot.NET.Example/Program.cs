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

        kahootClient.OnJoined += KahootClient_OnJoined;
        kahootClient.OnClientError += KahootClient_OnClientError;

       
        var validGame = await kahootClient.JoinAsync(result);

        if (!validGame)
        {
            Console.WriteLine("Could not find game");
        }

        await Task.Delay(-1);
    }

    private static Task KahootClient_OnClientError(object? sender, ClientErrorEventArgs arg)
    {
        Console.WriteLine(arg.Error);
        return Task.CompletedTask;
    }

    private static Task KahootClient_OnJoined(object? sender, JoinEventArgs args)
    {
        if (args.Success)
        {
            Console.WriteLine("Joined the game");
            Console.WriteLine($"Is2fa");
        }
        else 
        {
            Console.WriteLine("Failed to join game");
        }

        return Task.CompletedTask;
    }
}
