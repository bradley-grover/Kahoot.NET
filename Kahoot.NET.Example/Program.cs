using Kahoot.NET;
using Kahoot.NET.Client;
using Kahoot.NET.FluentBuilder;
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

       
        await kahootClient.JoinAsync(result);

        await Task.Delay(-1);
    }


    private static async Task Client_OnJoined(object? sender, EventArgs e)
    {
        Console.WriteLine("Stuff");
        await Task.Delay(1000);
    }
}
