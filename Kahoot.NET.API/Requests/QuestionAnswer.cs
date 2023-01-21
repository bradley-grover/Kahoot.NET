namespace Kahoot.NET.API.Requests;

public class QuestionAnswer : BaseClientMessage<GameContentData>
{
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
