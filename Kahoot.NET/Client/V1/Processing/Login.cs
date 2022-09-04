using Kahoot.NET.API.Requests.Login;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task SendLoginMessageAsync()
    {
        if (Username is null)
        {
            throw new InvalidOperationException("Username needs to be set to join");
        }

        await SendAsync(new LoginMessage()
        {
            Id = Interlocked.Increment(ref State.id).ToString(),
            ClientId = State.clientId,
            Data = new LoginInformation(
                Username, 
                GameId.ToString(), 
                JsonSerializer.Serialize(new { 
                    device = new Device() { Screen = Screen.Default, UserAgent = userAgent} 
                }))
        });
    }

    private async Task SendLastLoginMessageAsync()
    {
        await SendAsync(new FinalLoginMessage()
        {
            Id = Interlocked.Increment(ref State.id).ToString(),
            ClientId = State.clientId,
            Data = new FinalLoginInformation(GameId, JsonSerializer.Serialize(new { usingNamerator }))
        });
    }
}
