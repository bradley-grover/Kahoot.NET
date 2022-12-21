using Kahoot.NET.Client;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Example;

public class Program
{
    internal static readonly HttpClient httpClient = new();
    public static async Task Main(string[] args)
    {
        // simple console app

        // create a factory to produce a standard ILogger<T>
        var factory = LoggerFactory.Create(x => {
            x.AddConsole();
            x.SetMinimumLevel(LogLevel.Debug);
        });

        // has using statement to dispose underlying resources

        using IKahootClient kahootClient = new KahootClient(factory.CreateLogger<IKahootClient>(), httpClient);

        var code = await GetValidCodeAsync();

        // bind delegates to events from ClientEvents class, customise how you want to handle events in your own way
        kahootClient.Joined += ClientEvents.KahootClient_OnJoined;
        kahootClient.Left += ClientEvents.KahootClient_Left;


        var validGame = await kahootClient.JoinAsync(code, Random.Shared.Next(0, 999_999_999).ToString());

        await Task.Delay(-1);
    }

    public static async Task<int> GetValidCodeAsync()
    {
        while (true)
        {
            int result;

            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine($"Could not get numeric code, try again");
            }

            return result;
        }
    }
}
