using System.Runtime.InteropServices;

namespace Kahoot.NET.Mathematics;

[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct MathOperation
{
    public MathOperation(delegate*<long, long, long> operation)
    {
        Function = operation;
    }

    public readonly delegate*<long, long, long> Function;
}
