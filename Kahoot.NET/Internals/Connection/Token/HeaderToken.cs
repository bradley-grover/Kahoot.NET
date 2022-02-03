using System.Text;

namespace Kahoot.NET.Internals.Connection.Token;

internal class HeaderToken
{
    public static ReadOnlySpan<char> CreateHeaderToken(ReadOnlySpan<char> header)
    {
        Span<byte> span = Encoding.ASCII.GetBytes(header.ToArray());
        return Encoding.ASCII.GetString(span);
    }
}
