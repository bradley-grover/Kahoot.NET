namespace Kahoot.NET.Parsers;

/// <summary>
/// Represents a parser to retrieve data from a span of characters without allocating a new <see cref="string"/>
/// </summary>
internal interface IParser
{
    /// <summary>
    /// Parses the specified string to find a certain element
    /// </summary>
    /// <param name="input">The input to parse</param>
    /// <returns>The found element in the <see cref="ReadOnlySpan{T}"/></returns>
    ReadOnlySpan<char> Parse(ReadOnlySpan<char> input);
}

