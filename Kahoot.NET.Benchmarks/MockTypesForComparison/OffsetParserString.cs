namespace Kahoot.NET.Benchmarks.MockTypesForComparison;

public class StringOffsetParser
{
    public const string LookFor = "varoffset=";
    public string Parse(string input)
    {
        int indexOf = input.IndexOf(LookFor);


        int end = input[indexOf..].IndexOf(';') + indexOf;

        int combined = LookFor.Length + indexOf;


        return input[combined..end];
    }
}
