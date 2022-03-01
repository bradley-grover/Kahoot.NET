namespace Kahoot.NET.Internal.Token.Parsers;

internal class GetSpecialTokenParser : IParser
{
    const char Identifier = '\'';

    public ReadOnlySpan<char> Parse(ReadOnlySpan<char> input)
    {
        int first = input.IndexOf(Identifier);
        int last = input.LastIndexOf(Identifier);

        return input.Slice(++first, last - first);
    }
}
