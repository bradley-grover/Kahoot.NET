using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahoot.NET.Internals.Connection.Parsers;

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
