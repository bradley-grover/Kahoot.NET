using System.Diagnostics.CodeAnalysis;
using Kahoot.NET.API.Authentication;
using Kahoot.NET.Benchmarks.Alternatives;

namespace Kahoot.NET.Benchmarks.ToRun;

[BenchmarkModule("XOrStrings", "The result of ^ on strings")]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class BitwiseStrings
{
    // same or more length
    internal static readonly string _firstStr = "xdknFkJCaYrogVBGw3Sph6XkVtQnrPthkc95ish0mZ1YGqaZ8S6tjcArUaMxwO5jZNyEffxdgEjUxwmr3vxW9HRqxzDKX0XfNxm6";
    internal static readonly string _secondData = "K69lnBcymq0C6cUNp7zAoUDKAJy53edBmtyOJN0v9zsad0eTheQ5i0dRhTmJP6cs7X1h88jhZIdgxLMkU3dpF6fvt853H889qOP4";

    [Benchmark]
    public void Default()
    {
        char[] first = _firstStr.ToCharArray();
        char[] second = _secondData.ToCharArray();

        WebSocketKey.BitwiseOrSpans(first, second);
    }

    [Benchmark]
    public void Standard()
    {
        char[] first = _firstStr.ToCharArray();
        char[] second = _secondData.ToCharArray();

        WebSocketKeyAlternatives.BitwiseOrSpans(first, second);
    }
}
