namespace Kahoot.NET.Internals.Parsers;

/// <summary>
/// Parses the token part of the challenge token
/// </summary>
internal class SpecialParameterParser : IParser<char>
{
    const char Identifier = '\'';

    public ReadOnlySpan<char> Parse(ReadOnlySpan<char> input)
    {
        int first = input.IndexOf(Identifier);
        int last = input.LastIndexOf(Identifier);

        return input.Slice(++first, last - first);
    }
}
