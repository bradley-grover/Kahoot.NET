using Kahoot.NET.Client;
using Kahoot.NET.Client.Events;
using ParadoxTerminal;

namespace Kahoot.NET.HighLoadExample;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Enter game code: ");

        int code = Terminal.ReadInt32();

        Console.WriteLine("Enter number of clients");

        var count = Terminal.ReadInt32(1, int.MaxValue);

        var clients = new List<IKahootClient>(count);

        for (int i = 0; i < count; i++)
        {
            clients.Add(new KahootClient());
        }

        var tasks = new List<Task>();

        foreach (var client in clients)
        {
            client.Joined += Client_Joined;
            tasks.Add(client.JoinAsync(code, Random.Shared.Next(0, 999_999_999).ToString()));
        }

        await Task.WhenAll(tasks);

        await Task.Delay(-1);
    }

    private static Task Client_Joined(object? obj, JoinEventArgs args)
    {
        if (!args.IsSuccess)
        {
            Console.WriteLine(Enum.GetName(args.Result));
        }

        return Task.CompletedTask;
    }
}
