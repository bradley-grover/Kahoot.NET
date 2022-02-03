using System.Net.WebSockets;

namespace Kahoot.NET.Client;

/*
 * This partial class code file is used for the WebSocket connection to Kahoot
 */


public partial class KahootClient
{
    // TODO: FIND BEST WEBSOCKET CLIENT LIBRARY
    // private WebSocket Socket { get; }

    internal async Task CreateHandshakeAsync()
    {
        if (GameId is null)
        {
            throw new NoGameIdException();
        }

        string token = await Token.CreateTokenAsync(GameId.Value, Client);

        // create socket then join
        // "wss://kahoot.it/cometd/{gameid}/{token}"
    }
}
