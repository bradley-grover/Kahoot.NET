using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Kahoot.NET.Client;
using Kahoot.NET.Internal.Token;
using Xunit;

namespace Kahoot.NET.Tests;

public class ConnectionsTests
{
    [Theory]
    [InlineData(2162040)] // here should go the game code
    public async Task ConnectAsync(int gameCode)
    {
        (var token, var response) = await Token.CreateTokenAndSessionAsync(gameCode, new System.Net.Http.HttpClient());

        ClientWebSocket webSocket = new();


        Console.WriteLine(token);

        await webSocket.ConnectAsync(
            new Uri(string.Format(KahootClient.WebsocketUrl, gameCode, token)),
            System.Threading.CancellationToken.None);
    }
}
