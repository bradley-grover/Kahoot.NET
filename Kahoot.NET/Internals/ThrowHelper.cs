namespace Kahoot.NET.Internals;

/// <summary>
/// Providea static methods to throw exceptions under conditions
/// </summary>
internal static class ThrowHelper
{
    /// <summary>
    /// Throws if min is higher than max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void AssertInRange(int min, int max)
    {
        if (min > max)
        {
            throw new ArgumentOutOfRangeException(nameof(min));
        }
    }
    /// <summary>
    /// Throws if the span of data is empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <param name="paramName"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void ThrowIfEmpty<T>(ReadOnlySpan<T> input, 
        [CallerArgumentExpression("input")] string? paramName = null)
    {
        if (input.IsEmpty)
        {
            throw new ArgumentException($"{paramName} cannot be empty", paramName);
        }
    }
    /// <summary>
    /// Throws if a number exceeds a certain threshold
    /// </summary>
    /// <param name="threshold"></param>
    /// <param name="number"></param>
    /// <param name="paramName"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void ThrowOnExceed(int threshold, int number, 
        [CallerArgumentExpression("number")] string? paramName = null)
    {
        if (number > threshold)
        {
            throw new ArgumentException($"{paramName} cannot exceed {threshold}");
        }
    }
    /// <summary>
    /// Throws if the integer passed is not above zero
    /// </summary>
    /// <param name="value"></param>
    /// <param name="argName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void AssertAboveZero(int value, [CallerArgumentExpression("value")] string? argName = null)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(argName, $"{argName} must be above zero");
        }
    }
}
