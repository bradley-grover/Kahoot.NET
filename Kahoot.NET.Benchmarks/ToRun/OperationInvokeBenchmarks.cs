using System.Diagnostics.CodeAnalysis;
using Kahoot.NET.Mathematics;

namespace Kahoot.NET.Benchmarks.ToRun;

[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class OperationInvokeBenchmarks
{
    [Benchmark]
    public long Invoke_Operation()
    {
        long left = 10;
        long right = 20;

        return SimpleExpression.ApplyOperation('+', left, right);
    }

    [Benchmark]
    public unsafe long Invoke_UnmanagedDelegate()
    {
        long left = 10;
        long right = 20;

        delegate*<long, long, long> function = &Add;

        return function(left, right);
    }

    internal static long Add(long left, long right) => left + right;

    [Benchmark]
    public long Invoke_Normal()
    {
        long left = 10;
        long right = 20;

        return left + right;
    }

    [Benchmark]
    public unsafe long Invoke_UnmanagedDeleteDictionary()
    {
        long left = 10;
        long right = 20;

        return SimpleExpression._operations['+'].Function(left, right);
    }
}
