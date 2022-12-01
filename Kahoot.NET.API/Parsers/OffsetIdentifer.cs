namespace Kahoot.NET.Parsers;

/// <summary>
/// Finds the offset string
/// </summary>
internal class OffsetIdentifer : IParser
{
    private const string Start = "varoffset=";

    public ReadOnlySpan<char> Parse(ReadOnlySpan<char> input)
    {
        int indexOf = input.IndexOf(Start);

        int end = input[indexOf..].IndexOf(';') + indexOf;

        int combined = Start.Length + indexOf;

        return input[combined..end];
    }
}
