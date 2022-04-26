using Kahoot.NET;
using Kahoot.NET.Client;
using Kahoot.NET.Exceptions;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.ConsoleDemo;

public class Program
{
    private static readonly Random random = new();
    public static string GenerateName()
    {
        Span<char> name = new char[10];

        for (int i = 0; i < name.Length; i++)
        {
            name[i] = random.Next(0, 9).ToString()[0];
        }

        return new string(name);
    }

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

        IKahootClient client = new KahootClient(logger: loggerFactory.CreateLogger<IKahootClient>(), new HttpClient());

        int code = GetGameCode();
        try
        {
            await client.JoinAsync(code, GenerateName());
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
