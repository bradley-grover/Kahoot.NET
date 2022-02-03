using System.Data;
using System.Text;

namespace Kahoot.NET.Internals.Connection.Token;

internal static class ChallengeToken
{
    #region Parsers
    private static readonly IParser<char> Offset = new OffsetParser();
    private static readonly IParser<char> Parameter = new SpecialParameterParser();
    #endregion

    public static ReadOnlySpan<char> CreateToken(ReadOnlySpan<char> token)
    {
        var param = Parameter.Parse(token);

        var offset = GetOffset(Offset.Parse(token.ToString().RemoveWhitespace()));

        return Decode(param, offset);
    }


    internal static char Repl(char character, int position, long offset)
    {
        return Convert.ToChar(((((int)character*position)+offset)%77)+48);
    }


    private static DataTable Table { get; } = new();
    internal static long GetOffset(ReadOnlySpan<char> offsetString)
    {
        return (int)(Table.Compute(offsetString.ToString(), ""));
    }


    public static string Decode(ReadOnlySpan<char> value, long offset)
    {
        string content = value.ToString();

        StringBuilder builder = new();

        for (int i = 0; i < content.Length; i++)
        {
            builder.Append(Repl(content[i], i, offset));
        }

        return content;
    }

    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }
}
