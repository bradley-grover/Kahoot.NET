namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal const string WebsocketUrl = "wss://kahoot.it/cometd/{0}/{1}";
    internal string userAgent = RandomUserAgent.RandomUa.RandomUserAgent;
}
