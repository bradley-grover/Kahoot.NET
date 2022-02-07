namespace Kahoot.NET.Internals.Parsers;

/// <summary>
/// Parses the offset part of the challenge string
/// </summary>
internal class OffsetParser : IParser<char>
{
    private const string LookFor = "varoffset=";

    public ReadOnlySpan<char> Parse(ReadOnlySpan<char> input)
    {
        int indexOf = input.IndexOf(LookFor);


        int end = input[indexOf..].IndexOf(';') + indexOf;

        int combined = LookFor.Length + indexOf;


        return input[combined..end];
    }
}
