using Kahoot.NET.Shared;

namespace Kahoot.NET.Game.Client;

public interface IQuizCreator
{
    event Func<object?, EventArgs, Task>? QuizCreated;
    Task<int> CreateSessionAsync(string quizUrl, GameConfiguration? configuration = null, CancellationToken cancellationToken = default);
}
