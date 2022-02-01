namespace Kahoot.NET.Internals;

internal static class ThrowHelper
{
    public static void AssertInRange(int min, int max)
    {
        if (min > max)
        {
            throw new ArgumentOutOfRangeException(nameof(min));
        }
    }
}
