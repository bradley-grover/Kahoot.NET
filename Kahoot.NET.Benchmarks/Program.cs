// Kahoot.NET.Benchmarks
// To create a benchmark follow the format seen in Format.txt

// To run a benchmark use application.json or pass in through args the name of the benchmark module

using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using ParadoxTerminal;
using BenchmarkDotNet.Running;
using System.Transactions;

namespace Kahoot.NET.Benchmarks;

public class Program
{
    private static readonly Dictionary<string, Type> _types = GetTypes();
    public static async Task Main(string[] args)
    {
        Console.Title = "Kahoot.NET - Benchmarks";

        var types = new List<Type>();
        int iterations = 0;
        bool publishReportToDiscord = false;

        DiscordReporter.CreateClient("https://discord.com/api/webhooks/1050631915327201290/wjAoS2BOjeEjTmN0gb2g8YCWfPagfzIowTEc295wVTwypW6GH2_VMxqqZejsYFDRaNK4");

        if (args.Any())
        {
            // code for arguments passed in
        }
        else
        {
            types.Add(SelectType());

            Console.WriteLine("Type: (0) for default iteration count or enter a custom amount now");

            iterations = Terminal.ReadInt32(0, int.MaxValue);

            Console.WriteLine("Publish reports to Discord?");

            publishReportToDiscord = Terminal.ReadBool(BoolStyles.All);

            Console.WriteLine(publishReportToDiscord);
        }

        foreach (var type in types)
        {
            var summary = BenchmarkRunner.Run(type, new KahootBenchmarkConfig(iterations == 0 ? 50 : iterations));

            if (publishReportToDiscord)
            {
                try
                {
                    await DiscordReporter.SendMessageAsync(summary, type);
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not report results to discord");
                }
            }
        }
    }
    internal static Type SelectType()
    {
        Console.WriteLine("Pick a module to run a benchmark on");

        int number = 1;

        var indexTyped = _types.Select(x => x.Value).ToList();

        foreach (var item in indexTyped)
        {
            Console.WriteLine($"[{number++}] - {item.GetCustomAttribute<BenchmarkModuleAttribute>()!.Title}");
        }

        bool valid = false;
        string? input = null;
        bool isIndex = false;
        int index = 0;

        while (!valid)
        {
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Doesn't exist");
                continue;
            }

            if (int.TryParse(input.AsSpan(), out int result))
            {
                if (result < 1 || result > indexTyped.Count)
                {
                    Console.WriteLine($"Out of range, valid from 1 to {indexTyped.Count}");
                    continue;
                }

                index = result;
                valid = true;
                isIndex = true;
                break;
            }

            if (!_types.ContainsKey(input))
            {
                Console.WriteLine("Doesn't exist");
                continue;
            }

            valid = true;

            break;
        }

        return isIndex ? indexTyped[index - 1] : _types[input!];

    }

    public static Dictionary<string, Type> GetTypes()
    {
        var assembly = typeof(Program).Assembly;

        return assembly.GetTypes()
            .Where(t => t.IsClass)
            .Where(t => t.GetCustomAttribute<BenchmarkModuleAttribute>() != null)
            .ToDictionary(t => t.GetCustomAttribute<BenchmarkModuleAttribute>()!.Title);
    }
}
