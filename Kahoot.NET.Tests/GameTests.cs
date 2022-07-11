using Kahoot.NET.Client;
using Kahoot.NET.Game.Client;

namespace Kahoot.NET.Tests;

public class GameTests
{
    [Theory]
    [ClassData(typeof(ApprovedQuizzies))]
    public async Task Game_Should_Create(string url)
    {
        IQuizCreator creator = new QuizCreator(null);

        creator.QuizCreated += Creator_QuizCreated;

        int code = await creator.CreateSessionAsync(url);
    }

    [Theory]
    [ClassData(typeof(ApprovedQuizzies))]
    public async Task Game_Should_Be_Joinable(string url)
    {
        IQuizCreator creator = new QuizCreator(null);

        creator.QuizCreated += Creator_QuizCreated;

        int code = await creator.CreateSessionAsync(url);

        IKahootClient client = new KahootClient();

        await client.JoinAsync(code, "hello");

        await client.LeaveAsync();
    }

    private Task Creator_QuizCreated(object? sender, EventArgs args)
    {
        Assert.NotNull(sender);

        return Task.CompletedTask;
    }
}
