using Kahoot.NET.API.Requests.Login;
using Kahoot.NET.API;
using Kahoot.NET.API.Shared;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task SendLoginMessageAsync()
    {
        await SendAsync(new LoginMessage()
        {
            Id = Interlocked.Increment(ref State.id).ToString(),
            ClientId = State.clientId,
            Data = new LoginInformation(
                Username ?? Random.Shared.Next(0, 999_999_999).ToString(), 
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
