using Kahoot.NET.Internal.Token;
using Kahoot.NET.Internal.Token.Processing;

namespace Kahoot.NET.Tests;

public class DecodeTokenTests
{
    [Theory]
    [ClassData(typeof(ChallengeTokens))]
    public void DecodeToken(string header, string challengeToken, string expected)
    {
        var actual = Merger.Create(Header.CreateHeaderToken(header), Challenge.CreateToken(challengeToken));

        Assert.Equal(expected, actual);
    }
}
