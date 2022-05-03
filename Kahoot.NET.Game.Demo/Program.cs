using Kahoot.NET.Game.Client;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Game.Demo;

public class Program
{
    public static async Task Main(string[] args)
    {
        var logFactory = LoggerFactory.Create(x =>
        {
            x.AddConsole();
            x.SetMinimumLevel(LogLevel.Debug);
        });

        IQuizCreator creator = new QuizCreator(logFactory.CreateLogger<IQuizCreator>());

        Console.WriteLine(await creator.CreateSessionAsync("https://play.kahoot.it/v2/lobby?quizId=faf45afa-050b-440e-881b-4845801df788"));


        await Task.Delay(-1);
    }
}
