using Kahoot.NET.API.Authentication.Token;

namespace Kahoot.NET.Tests;

public class DecodeTokenTests
{
    [Theory]
    [ClassData(typeof(ChallengeTokens))]
    public void DecodeToken(string header, string challengeToken, string expected)
    {
        var actual = Key.Create(header, challengeToken);

        Assert.Equal(expected, actual);
    }
}
