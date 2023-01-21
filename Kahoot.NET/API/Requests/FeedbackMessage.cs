namespace Kahoot.NET.API.Requests;

public class FeedbackMessage : BaseClientMessage<GameHostContentData>
{
    public FeedbackMessage(Feedback feedback, uint code)
    {
        Channel = Channels.Service;
        Ext = new(); // empty object "{ }"

        Data = new()
        {
            Id = (uint)LiveEventId.SendFeedback,
            Type = Types.Message,
            Content = JsonSerializer.Serialize(feedback),
            GameId = code.ToString(),
        };
    }

#nullable disable
    [JsonPropertyName("ext")]
    public object Ext { get; set; }
}
