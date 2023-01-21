using Kahoot.NET.API.Authentication;
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
        kahootClient.QuestionReceived += ClientEvents.KahootClient_QuestionRec;
        kahootClient.FeedbackRequested += ClientEvents.KahootClient_OnFeedbackRequest;

        var validGame = await kahootClient.JoinAsync(code, Random.Shared.Next(0, 999_999_999).ToString());

        await Task.Delay(-1);
    }


    public static async Task<uint> GetValidCodeAsync()
    {
        Console.WriteLine("Enter game code:");

        while (true)
        {
            uint result;

            while (!uint.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine($"Could not get numeric code, try again");
            }

            if (await Request.GameExistsAsync(httpClient, result))
            {
                return result;
            }

            Console.WriteLine("Game doesn't exist");
        }
    }
}
