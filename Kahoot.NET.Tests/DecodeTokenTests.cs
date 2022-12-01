using Kahoot.NET.API.Authentication;

namespace Kahoot.NET.Tests;

[Trait(Traits.Parsers, Traits.ParsersDesc)]
public class DecodeTokenTests
{
    [Theory(DisplayName = "Decodes the challenge function and header into websocket key")]
    [ClassData(typeof(ChallengeTokens))]
    public void DecodeToken(string header, string challenge, string expected)
    {
        var actual = Key.Create(header, challenge);

        Assert.Equal(expected, actual);
    }
}
