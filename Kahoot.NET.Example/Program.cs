using Kahoot.NET;
using Kahoot.NET.Client;
using Kahoot.NET.FluentBuilder;

namespace Kahoot.NET.Example;

public class Program
{
    public static async Task Main(string[] args)
    {
        // wiped for redesign
        await Task.CompletedTask;
    }

    private static async Task Client_OnJoined(object? sender, EventArgs e)
    {
        Console.WriteLine("Stuff");
        await Task.Delay(1000);
    }
}
