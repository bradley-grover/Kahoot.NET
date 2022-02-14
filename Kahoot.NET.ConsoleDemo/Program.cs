using Kahoot.NET;
using Kahoot.NET.Client;
using Kahoot.NET.Internals.Connection;
using Kahoot.NET.Internals.Parsers;
using Kahoot.NET.Internals.Connection.Token;
using Kahoot.NET.Shared;
using Kahoot.NET.FluentBuilder;
using System.Net.WebSockets;
using Kahoot.NET.Internals.Messages;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kahoot.NET.Internals.Messages.Handshake;

namespace Kahoot.NET.ConsoleDemo;

public class Program
{
    public static async Task Main(string[] args)
    {
        await RunConnectionTestAsync(int.Parse(Console.ReadLine()!));
    }

    public static async Task RunConnectionTestAsync(int code)
    {
        string token = await Token.CreateTokenAsync(code, new());

        ClientWebSocket socket = new();

        Uri uri = new("wss://kahoot.it/cometd/7841538/2d36304551208acb52724709ff04f6c451d08d0f985e70049af7821e9ef92e89d9c876cfb82aa0bfdd6105388fc5a58e");

        Uri other = new Uri(string.Format("wss://kahoot.it/cometd/{0}/{1}", code, token));

        Console.WriteLine(other);

        await socket.ConnectAsync(uri, CancellationToken.None);

        Memory<byte> buffer = new byte[512];

        string content = JsonSerializer.Serialize(new FirstHandshake());

        var message = new ArraySegment<byte>(Encoding.UTF8.GetBytes(content));

        await socket.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None);

        while (socket.State == WebSocketState.Open)
        {

            var res = await socket.ReceiveAsync(buffer, CancellationToken.None);
            if (res.MessageType == WebSocketMessageType.Close)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            Content(buffer, res.Count);
        }

    }

    public static void Content(Memory<byte> data, int count)
    {
        Console.WriteLine(Encoding.UTF8.GetString(data.ToArray(), 0, count));
    }

    private static void WriteLineEmpty(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine();
        }
    }
}
