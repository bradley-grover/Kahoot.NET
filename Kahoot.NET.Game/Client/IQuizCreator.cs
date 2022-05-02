namespace Kahoot.NET.Game.Client;

public interface IQuizCreator
{
    Task<int> CreateSessionAsync(string quizUrl, GameConfiguration? configuration = null, CancellationToken cancellationToken = default);
}
