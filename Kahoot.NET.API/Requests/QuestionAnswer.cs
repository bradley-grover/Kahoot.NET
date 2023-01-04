namespace Kahoot.NET.API.Requests;

public class QuestionAnswer : BaseClientMessage<GameContentData>
{
    public QuestionAnswer(QuestionAnswerContent content, int gameId)
    {
        Data = new()
        {
            Content = JsonSerializer.Serialize(content),
            GameId = gameId.ToString(),
            Id = (int)LiveEventId.AnswerQuestion,
            Type = Types.Message
        };

        Channel = Channels.Service;
    }
}
