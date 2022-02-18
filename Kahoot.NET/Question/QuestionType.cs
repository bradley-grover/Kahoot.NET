namespace Kahoot.NET.Question;


/// <summary>
/// The type of question sent to the client
/// </summary>
public enum QuestionType
{
    /// <summary>
    /// The question type is standard quiz with 2-4 results
    /// </summary>
    Quiz,
    /// <summary>
    /// The question type is a survery where you can answer with text
    /// </summary>
    Survey,
    /// <summary>
    /// 
    /// </summary>
    Content,
    /// <summary>
    /// 
    /// </summary>
    Jumble,
    /// <summary>
    /// 
    /// </summary>
    OpenEnded,
    /// <summary>
    /// 
    /// </summary>
    WordCloud
}
