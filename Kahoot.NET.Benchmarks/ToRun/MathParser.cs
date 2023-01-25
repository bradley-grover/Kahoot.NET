using System.Diagnostics.CodeAnalysis;
using Jace;
using Kahoot.NET.Benchmarks.Alternatives;
using Kahoot.NET.Mathematics;
using AngouriMath;

namespace Kahoot.NET.Benchmarks.ToRun;

[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class MathParser
{
    [Benchmark]
    public long Eval()
    {
        return SimpleExpression.Evaluate(Mock.OffsetString);
    }

    [Benchmark]
    public long Angouri_Eval()
    {
        Entity entity = Mock.OffsetString;

        return (long)entity.EvalNumerical();
    }

    internal static readonly CalculationEngine _engine = new();

    [Benchmark]
    public long Jace()
    {
        return (long)_engine.Calculate(Mock.OffsetString);
    }

    [Benchmark]
    public long DataTable_Regex()
    {
        return MathParserAlternatives.Parse(Mock.OffsetString);
    }
}
