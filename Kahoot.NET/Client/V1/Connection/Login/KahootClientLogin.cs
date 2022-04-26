using Kahoot.NET.Internal.Data.Messages.Login;

namespace Kahoot.NET.Client;


public partial class KahootClient
{
    private static readonly Random random = new();
    private async Task SendLoginAsync()
    {
        await SendAsync(new LoginMessage()
        {
            Channel = LiveMessageChannels.Service,
            Id = Interlocked.Increment(ref _sessionObject.id).ToString(),
            Ext = new { },
            LoginData = new LoginData()
            {
                Content = JsonSerializer.Serialize(new
                {
                    device = new UserDeviceModel()
                    {
                        Screen = new() { Width = 1920, Height = 1080 },
                        UserAgent = userAgent,
                    }
                }),
                GameId = GameId.ToString(),
                Name = Username ?? random.NextInt64(0, 999_999_999_999_999).ToString(),
            },
            ClientId = _sessionObject.clientId
        });
    }
}
