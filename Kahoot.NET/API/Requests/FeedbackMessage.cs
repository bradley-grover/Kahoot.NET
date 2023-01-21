namespace Kahoot.NET.API.Requests;

/// <summary>
/// Message to send to Kahoot! after the host has requested it
/// </summary>
public class FeedbackMessage : BaseClientMessage<GameHostContentData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedbackMessage"/> using the specified parameters
    /// </summary>
    /// <param name="feedback">The feedback to be used</param>
    /// <param name="code">The game code</param>
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
    /// <summary>
    /// Empty object required by Kahoot!
    /// </summary>
    [JsonPropertyName("ext")]
    public object Ext { get; set; }
}
