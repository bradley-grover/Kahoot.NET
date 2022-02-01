using WebSocketSharp;

namespace Kahoot.NET.Client;

/*
 * This partial class code file is used for the WebSocket connection to Kahoot
 */


public partial class KahootClient
{
    private WebSocket? Socket { get; set; }

    internal async Task CreateHandshakeAsync()
    {
        if (GameId is null)
        {
            throw new NoGameIdException();
        }

        string token = await Token.CreateTokenAsync(GameId.Value, Client);

        Socket = new($"wss://kahoot.it/cometd/{GameId.Value}/{token}");


        //Socket.Connect();

    }
}
