namespace Kahoot.NET.Internal.Token.Parsers;

internal interface IValueParser<T>
{
    T Parse(ReadOnlySpan<char> input);
}
