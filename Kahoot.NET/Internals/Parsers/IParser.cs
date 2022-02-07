namespace Kahoot.NET.Internals.Parsers;

/// <summary>
/// Parses data from a string into another slice of other data
/// </summary>
/// <typeparam name="T"></typeparam>
internal interface IParser<T>
{
    /// <summary>
    /// Parses data back into <see cref="ReadOnlySpan{T}"/>
    /// </summary>
    /// <param name="input"></param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/></returns>
    ReadOnlySpan<T> Parse(ReadOnlySpan<char> input);
}
