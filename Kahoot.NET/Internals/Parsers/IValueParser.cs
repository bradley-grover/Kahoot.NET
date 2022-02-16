namespace Kahoot.NET.Internals.Parsers;

/// <summary>
/// Parses data from a span into <typeparamref name="T"/>
/// </summary>
/// <typeparam name="T"></typeparam>
internal interface IValueParser<out T>
{
    /// <summary>
    /// Parses a span into T
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    T Parse(ReadOnlySpan<char> input);
}
