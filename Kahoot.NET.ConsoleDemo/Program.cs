using Kahoot.NET;
using Kahoot.NET.Client;

namespace Kahoot.NET.ConsoleDemo;

public class Program
{
    public static async Task Main(string[] args)
    {
        int code = Convert.ToInt32(Console.ReadLine());

        KahootClient client = new(new HttpClient());

        client.GameId = code;

        await client.CreateHandshakeAsync();
    }
}
