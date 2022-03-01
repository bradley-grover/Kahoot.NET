namespace Kahoot.NET.Internal.Token.Parsers;

internal interface IParser
{
    ReadOnlySpan<char> Parse(ReadOnlySpan<char> input);
}
