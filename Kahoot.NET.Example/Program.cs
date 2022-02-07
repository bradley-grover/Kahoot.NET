using Kahoot.NET;
using Kahoot.NET.Client;
using Kahoot.NET.FluentBuilder;

namespace Kahoot.NET.Example;

public class Program
{
    public static async Task Main(string[] args)
    {
        // keep reading from standard input until we get an 32 bit integer
        int code;
        while (true)
        {
            Console.WriteLine("Enter code:");
            if (!int.TryParse(Console.ReadLine(), out code))
            {
                continue;
            }
            break;
        }

        // keep reading from standard input until we get a not null value

        string? name = null;

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Enter name: ");
            name = Console.ReadLine();
        }

        /* 
         * Examples
         * the IKahootClient is under the Kahoot.NET.Client namespace so to use it add a using statement for it
         * all of these achieve roughly the same outcome
         */

        /*
         * Example One:
         * - note that the config can be passed as null and it will use the default and work all fine
         * - also note that while here we passed a new HttpClient i would reccommend using a singleton,
         * or use the other constructor which takes a IHttpClientFactory
         * not using these methods could leave to thread starvation on large amounts of bots as they all 
         * try to join
         */

        // can use using statement as IKahootClient must implement IDisposable
        using IKahootClient client = new KahootClient(config: null, new HttpClient());

        // here you could bind certain events before we connect
        client.OnJoined += Client_OnJoined;

        // then here we can actually join like this

        await client.JoinAsync(code, name);

        // now client.OnJoined should have fired and if we bind other events is where stuff will actually happen

        /*
         * Example 2: static class Kahoot
         * In this example we use the static class as the creation method
         * this is preferred over the first one as in the future if KahootClient is replaced
         * with say BetterKahootClient your code will update to the new client without changing anything
         */

        using IKahootClient factoryClient = Kahoot.CreateClient(); // on this method call we could add a config

        // here we could do the same thing as before but now its future proofed as well

        /*
         * Example 3: fluent builder
         * this requires a using statement for Kahoot.NET.FluentBuilder
         */
        using IKahootClient fluent = FluentClientBuilder.New()
            .WithHttpClient(new()) // can add whatever HttpClient
                                   //.WithLogger() we can skip this method call if we want and it would still work fine
            .Build(); // make sure to call Build at the end of the chain

        // other:
        // with the client you can disable certain modules for potential perfomance gains

        /*
         * for example if you dont pass a config KahootClientConfig.Default is used
         * we could change the values of the default like this
         */
        KahootClientConfig.Default.EnableReconnect = false;


        /*
         * modules that are disabled will throw an InvalidOperationException if you try to use them
         */
        // another way of doing it would be explicity stating the config like this

        KahootClientConfig config = new();
        config.ReadOnlyData = true;

        // pass in here, even if your config is null it will just use the default
        using IKahootClient explicitPass = new KahootClient(config, new HttpClient());
    }

    private static void Client_OnJoined(object? sender, EventArgs e)
    {
        Console.WriteLine("I joined the kahoot!");
    }
}
