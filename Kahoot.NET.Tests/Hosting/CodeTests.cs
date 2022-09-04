using Kahoot.NET.Client;

namespace Kahoot.NET.Tests.Hosting;

[Trait(Traits.Code, Traits.CodeDesc)]
public class CodeTests
{
    [Theory]
    [InlineData("https://kahoot.it?pin=920046&refer_method=link", 920046)]
    [InlineData("https://kahoot.it?pin=134566", 134566)]
    public void ParseGameTest_Works(string gameLink, int gameCode)
    {
        bool s = Code.TryGetCode(gameLink, out var c);

        Assert.True(s);
        Assert.Equal(gameCode, c);
    }

    [Theory]
    [InlineData("https:")]
    [InlineData("https://kah00t.it?pin=1")]
    public void ParseGame_ShouldFail(string input)
    {
        Assert.False(Code.TryGetCode(input, out _));
    }

    [Theory]
    [ClassData(typeof(ApprovedQuizzies))]
    public async void CodeShould_ReturnTrue(string url)
    {
        var host = TestHelper.CreateHost();

        var code = await host.CreateGameAsync(new Uri(url));

        var httpClient = new HttpClient();

        Assert.True(await Code.ExistsAsync(code, httpClient));
    }
}
