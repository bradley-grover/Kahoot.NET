// Kahoot.NET.Benchmarks
// To create a benchmark follow the format seen in Format.txt

// To run a benchmark use application.json or pass in through args the name of the benchmark module

using System.Reflection;
using BenchmarkDotNet.Running;

namespace Kahoot.NET.Benchmarks;

public class Program
{
    private static readonly Dictionary<string, Type> _types = GetTypes();

    public static void Main(string[] args)
    {
        var types = new List<Type>();

        if (args.Any())
        {
            
        }
        else
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

            if (isIndex)
            {
                types.Add(indexTyped[index-1]);
            }
            else
            {
                types.Add(_types[input!]);
            }
        }

        foreach (var type in types)
        {
            BenchmarkRunner.Run(type);
        }
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
