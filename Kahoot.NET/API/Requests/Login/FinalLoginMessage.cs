namespace Kahoot.NET.API.Requests.Login;

/// <summary>
/// The final message to send for logging into the game
/// </summary>
internal class FinalLoginMessage : BaseClientMessage<FinalLoginInformation>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FinalLoginMessage"/> class
    /// </summary>
    public FinalLoginMessage()
    {
        Channel = Channels.Service;
        Ext = new { };
    }

    /// <summary>
    /// Empty object for passing to the websocket
    /// </summary>
    [JsonPropertyName("ext")]
    public object Ext { get; set; }
}
