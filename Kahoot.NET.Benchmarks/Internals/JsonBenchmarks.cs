using System.Text.Json;
using Kahoot.NET.Internal.Data.Messages;
using Kahoot.NET.Internal.Data.SourceGenerators.Messages;

namespace Kahoot.NET.Benchmarks.Internals;

[MemoryDiagnoser]
public class JsonBenchmarks
{
    private readonly LiveBaseMessage Message = new()
    {
        Channel = "/handshake",
        Id = "2",
    };
    [Benchmark]
    public void Without_SourceGenerator()
    {
        JsonSerializer.Serialize(Message);
    }
    [Benchmark]
    public void With_SourceGeneration()
    {
        JsonSerializer.Serialize(Message, LiveBaseMessageContext.Default.LiveBaseMessage);
    }
}
