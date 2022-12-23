using Kahoot.NET.API.Requests.Login;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ProcessChannelAsync(ReadOnlyMemory<byte> content, string channel, string? data = null)
    {
        if (data is null) return; // allow nulls to be passed but ignore them

        switch (channel)
        {
            case Channels.Connect:
                await ReplyAsync();
                break;
            case Channels.Status:
                await StatusAsync(content, data);
                break;
            case Channels.Service:
                await ServiceAsync(content, data);
                break;
            case Channels.Player:
                await PlayerAsync(content, data);
                break;
        }
    }

    internal async Task StatusAsync(ReadOnlyMemory<byte> content, string data)
    {
        var statusResponse = JsonSerializer.Deserialize<Message<StatusResponse>>(content.Span);

        Debug.Assert(statusResponse != null);

        switch (data)
        {
            case Types.Status:
                switch (statusResponse.Data!.Status)
                {
                    case Types.Active: // successful join notify client
                        Debug.WriteLine("Kahoot Client is active");
                        await Joined.InvokeEventAsync(this, new(JoinResult.Success));
                        break;
                    case Types.Errors.Locked:
                        Debug.WriteLine("Kahoot Client has tried to enter a locked game");
                        await LeaveAsync(LeaveCondition.Locked);
                        break;
                    case Types.Errors.Queue:
                        Debug.WriteLine("Kahoot client has tried entering a full game");
                        await LeaveAsync(LeaveCondition.Full);
                        break;
                        // add more later
                }
                break;
        }
    }

    internal async Task ServiceAsync(ReadOnlyMemory<byte> content, string data)
    {
        switch (data)
        {
            case Types.LoginResponse:
                var error = JsonSerializer.Deserialize<Message<DataErrorResponse>>(content.Span)!;

                if (error.Data!.Error != null)
                {
                    switch (error.Data.Error)
                    {
                        case Types.Errors.UserInput:
                            await Joined.InvokeEventAsync(this, new(JoinResult.DuplicateUserName));
                            await LeaveAsync(LeaveCondition.JoinFailure);
                            break;
                    }
                }

                await SendAsync(new FinalLoginMessage()
                {
                    Id = Interlocked.Increment(ref _stateObject.id).ToString(),
                    ClientId = _stateObject.clientId,
                    Data = new FinalLoginInformation(_code, JsonSerializer.Serialize(new { _usingNamerator }))
                });

                break;
        }
    }

    internal async Task PlayerAsync(ReadOnlyMemory<byte> content, string data)
    {
        var userObject = JsonSerializer.Deserialize<Message<ContentData>>(content.Span);

        Debug.Assert(userObject != null);

        switch (data)
        {
            case Types.Message:
                switch (userObject.Data!.Id)
                {
                    case 10: // TODO: Add event notifier
                        await LeaveAsync(LeaveCondition.Kicked);
                        break;
                }
                break;
        }
    }
}
