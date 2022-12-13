using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;

namespace Kahoot.NET.Benchmarks;

public class KahootBenchmarkConfig : ManualConfig
{
    public KahootBenchmarkConfig(int timesToRun)
    {
        AddDiagnoser(MemoryDiagnoser.Default);
        AddLogger(ConsoleLogger.Default);
        AddExporter(HtmlExporter.Default);
        AddColumnProvider(DefaultColumnProviders.Instance);

        AddJob(GetJob(timesToRun).WithRuntime(CoreRuntime.Core60));
        AddJob(GetJob(timesToRun).WithRuntime(CoreRuntime.Core70));
    }

    internal static Job GetJob(int timesToRun)
    {
        return Job.Default
            .WithPlatform(Platform.X64)
            .WithJit(Jit.RyuJit)
            .WithIterationCount(timesToRun)
            .WithStrategy(RunStrategy.Throughput);
    }
}
