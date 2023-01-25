// Kahoot.NET.Benchmarks
// To create a benchmark follow the format seen in Format.txt

using BenchmarkDotNet.Running;

namespace Kahoot.NET.Benchmarks;

public class Program
{
    public static async Task Main(string[] args)
    {
        string? webhookUrl = Environment.GetEnvironmentVariable("DISCORD_WEBHOOK_URL");

        KahootBenchmarkConfig configuration;

        if (args.Length == 1)
        {
            configuration = new(int.Parse(args[0].AsSpan()));
        }
        else
        {
            configuration = new(50);
        }

        var reports = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(config: configuration);

        if (webhookUrl is null) return;

        DiscordReporter.CreateClient(webhookUrl);

        await Task.Delay(5000); // give a little time to connect

        foreach (var report in reports)
        {
            await DiscordReporter.SendMessageAsync(report);
        }
    }
}
