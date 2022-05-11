using Kahoot.NET.Game.Client;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Game.Demo;

public class Program
{
#nullable disable
    private static ILogger<Program> Logger;
#nullable restore

    public static async Task Main(string[] args)
    {
        var logFactory = LoggerFactory.Create(x =>
        {
            x.AddConsole();
            x.SetMinimumLevel(LogLevel.Debug);
        });
        Logger = logFactory.CreateLogger<Program>();

        IQuizCreator creator = new QuizCreator(logFactory.CreateLogger<IQuizCreator>());

        Console.WriteLine(await creator.CreateSessionAsync("https://play.kahoot.it/v2/lobby?quizId=faf45afa-050b-440e-881b-4845801df788"));

        creator.QuizCreated += Creator_QuizCreated;

        await Task.Delay(-1);
    }

    private static Task Creator_QuizCreated(object? sender, EventArgs e)
    {
        Logger.LogInformation("I have created the game");
        return Task.CompletedTask;
    }
}
