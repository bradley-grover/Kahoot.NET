namespace Kahoot.NET.API.Requests;

/// <summary>
/// Represents a question answer, this is used internally to send the message to Kahoot
/// </summary>
public class QuestionAnswer : BaseClientMessage<GameContentData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestionAnswer"/> class with the specified parameters
    /// </summary>
    /// <param name="content">The content used to answer the question</param>
    /// <param name="gameId">The code of the game</param>
    public QuestionAnswer(QuestionAnswerContent content, uint gameId)
    {
        Data = new()
        {
            Content = JsonSerializer.Serialize(content),
            GameId = gameId.ToString(),
            Id = (uint)LiveEventId.AnswerQuestion,
            Type = Types.Message
        };

        Channel = Channels.Service;
    }
}
