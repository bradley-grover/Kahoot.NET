namespace Kahoot.NET.Mathematics;

internal static class MathOperations
{
    public static long Add(long left, long right) => left + right;
    public static long Subtract(long left, long right) => left - right;
    public static long Multiply(long left, long right) => left * right;
    public static long Divide(long left, long right) => left / right;
    public static long Pow(long left, long right) => (long)Math.Pow(left, right);
}
