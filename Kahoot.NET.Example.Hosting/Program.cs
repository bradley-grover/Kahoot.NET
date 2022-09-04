using Kahoot.NET.Hosting.Client;
using Microsoft.Extensions.Logging;
using ParadoxTerminal;

namespace Kahoot.NET.Example.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var factory = LoggerFactory.Create(x => {
            x.AddConsole();
            x.SetMinimumLevel(LogLevel.Debug);
        });

        IKahootHost host = new KahootHost(factory.CreateLogger<IKahootHost>());

        string? input = null;
        bool valid = false;

        while (input is null && !valid)
        {
            input = await Terminal.ReadLineAsync();

            valid = Uri.IsWellFormedUriString(input, UriKind.Absolute);
        }

        int code = await host.CreateGameAsync(new Uri(input!));

        Console.WriteLine($"The code is {code}, waiting 20 seconds to start");

        await Task.Delay(20*1000);

        await Task.Delay(-1);
    }
}
