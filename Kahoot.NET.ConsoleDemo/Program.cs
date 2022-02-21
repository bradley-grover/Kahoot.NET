using Kahoot.NET;
using Kahoot.NET.Client;
using Kahoot.NET.Exceptions;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.ConsoleDemo;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .SetMinimumLevel(LogLevel.Debug)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole();
        });
        var logger = loggerFactory.CreateLogger<Program>();

        IKahootClient client = new KahootClient(loggerFactory.CreateLogger<IKahootClient>(), new());

        int code = GetGameCode();
        try
        {
            await client.JoinAsync(code, "ok");
            logger.LogInformation("Am here");
        }
        catch (GameNotFoundException)
        {
            Console.WriteLine("Could not find game");
            Thread.Sleep(1000);
            await Main(args);
        }
        await Task.Delay(-1);
        
    }
    private static int GetGameCode()
    {
        int code;

        Console.WriteLine("code: ");

        while (!int.TryParse(Console.ReadLine().AsSpan(), out code))
        {
            Console.WriteLine("code: ");
            Console.Clear();
            Thread.Sleep(1000);
        }

        return code;
    }
}
