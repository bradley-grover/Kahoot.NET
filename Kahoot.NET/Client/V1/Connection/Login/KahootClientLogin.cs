using Kahoot.NET.Internal.Data.Messages.Login;
using Kahoot.NET.Internal.Data.Responses.Login;

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

    private async Task SendSecondLoginAsync()
    {
        await SendAsync(new LoginReply()
        {
            ClientId = _sessionObject.clientId,
            Ext = new { },
            Data = new
            {
                content = JsonSerializer.Serialize(new { usingNamerator } ),
                gameid = GameId,
                host = "kahoot.it",
                id = 16,
                type = "message"
            },
            Id = "5",
        });
    }

    private async Task HandleLoginResponseAsync(LoginResponse response)
    {
        if (response.Data.CId is not null)
        {
            await SendSecondLoginAsync();
            return;
        }

        switch (response.Data.Type)
        {
            case "USER_INPUT":
                throw new DuplicateNameException(Username!);
            case "NONEXISTING_SESSION":
                throw new NoSessionFoundException();
            case "RESTART":
                throw new CouldNotEstablishConnectionException();
        }
    }
}
