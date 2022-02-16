namespace Kahoot.NET.Question;


/// <summary>
/// Represents a question in Kahoot
/// </summary>
public interface IQuestion
{
    /// <summary>
    /// If the question ended or not
    /// </summary>
    bool Ended { get; }
    /// <summary>
    /// The <see cref="Shared.GameMode"/> of the see <see cref="IQuestion"/>
    /// </summary>
    GameMode GameMode { get; }
    /// <summary>
    /// The number of the question
    /// </summary>
    int Number { get; }
    /// <summary>
    /// The position in the quiz of the question
    /// </summary>
    int Position { get; }
    /// <summary>
    /// The type of question
    /// </summary>
    QuestionType QuestionType { get; }
    /// <summary>
    /// The time left before the question ends
    /// </summary>
    TimeSpan TimeLeft { get; }

    /// <summary>
    /// Method to answer the <see cref="IQuestion"/>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task AnswerAsync(int id);
}
