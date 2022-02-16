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
using Kahoot.NET.Internals.Responses;
using System.Diagnostics;

namespace Kahoot.NET.ConsoleDemo;

public class Program
{
    public static async Task Main(string[] args)
    {
        IKahootClient client = new KahootClient(KahootClientConfig.Default, new HttpClient());

        int code = int.Parse(Console.ReadLine()!);


        await client.JoinAsync(code, "nice");
    }

}
