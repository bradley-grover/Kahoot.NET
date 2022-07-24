namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private bool _inGame = false;
    internal string userAgent = RandomUserAgent.RandomUa.RandomUserAgent;
    private bool usingNamerator;
}
