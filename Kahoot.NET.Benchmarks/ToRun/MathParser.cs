using System.Diagnostics.CodeAnalysis;
using Jace;
using Kahoot.NET.API;

namespace Kahoot.NET.Benchmarks.ToRun;

[BenchmarkModule("MathParser", "Testing a built math parser for performance gains potentially")]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class MathParser
{
    [Benchmark]
    public void Eval()
    {
        Evaluator.Evaluate(Mock.OffsetString);
    }

    internal static readonly CalculationEngine _engine = new();

    [Benchmark]
    public void Jace()
    {
        _engine.Calculate(Mock.OffsetString);
    }
}
