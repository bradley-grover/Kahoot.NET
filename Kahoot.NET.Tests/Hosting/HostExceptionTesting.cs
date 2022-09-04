using Kahoot.NET.Hosting.Client.Errors;

namespace Kahoot.NET.Tests.Host;

[Trait(Traits.Host, Traits.HostDesc)]
public class HostExceptionTesting
{
    [Fact(DisplayName = "Argument Testing - Null")]
    public async Task Should_Throw_OnNull_Uri_Create()
    {
        var host = TestHelper.CreateHost();

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await host.CreateGameAsync(null!));
    }

    [Fact(DisplayName = "Argument Testing")]
    public async Task Should_Throw_OnBad_Uri()
    {
        var host = TestHelper.CreateHost();

        await Assert.ThrowsAsync<QuizNotFoundException>(async () => {
            await host.CreateGameAsync(new Uri("https://google.com"));
        });
    }
}
