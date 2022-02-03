[assembly: InternalsVisibleTo("Kahoot.NET.ConsoleDemo")]

namespace Kahoot.NET.Internals.Connection.Parsers;

internal interface IParser<T>
{
    ReadOnlySpan<T> Parse(ReadOnlySpan<char> input);
}
internal interface IValueParser<T>
{
    T ParseTo(ReadOnlySpan<char> input);
}
