namespace Kahoot.NET.Question;

public interface IQuestion
{
    bool Ended { get; }
    GameMode GameMode { get; }
    int Number { get; }
    int Position { get; }
    QuestionType QuestionType { get; }
    TimeSpan TimeLeft { get; }

    Task AnswerAsync(int id);
}
