using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahoot.NET.Internals.Connection.Parsers;

internal class OffsetParser : IParser<char>
{
    private readonly string lookFor = "varoffset=";

    public ReadOnlySpan<char> Parse(ReadOnlySpan<char> input)
    {
        int indexOf = input.IndexOf(lookFor);


        int end = input[indexOf..].IndexOf(';') + indexOf;

        int combined = lookFor.Length + indexOf;


        return input[combined..end];
    }
}
