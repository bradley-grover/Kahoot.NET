namespace Kahoot.NET.Parsers;

/// <summary>
/// Parses a string into a certain type with a value
/// </summary>
/// <typeparam name="T">The value to extract</typeparam>
internal interface IValueParser<T>
{
    /// <summary>
    /// Parses a span of characters into a value
    /// </summary>
    /// <param name="input">The input to parses</param>
    /// <returns>The desired value</returns>
    T Parse(ReadOnlySpan<char> input);
}
