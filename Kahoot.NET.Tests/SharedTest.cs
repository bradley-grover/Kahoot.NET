using System;
using Kahoot.NET.Shared;
using Xunit;

namespace Kahoot.NET.Tests;

public class SharedTest
{
    [Fact]
    public void RemoveWhitespaceTest()
    {
        ReadOnlySpan<char> originalString = "a b c d";

        originalString = originalString.RemoveWhitespace();

        Assert.Equal("abcd", originalString.ToString());
    }
}
