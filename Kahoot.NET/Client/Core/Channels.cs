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
                        await Joined.InvokeEventAsync(this, new(JoinResult.Success, _code));
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
                switch ((LiveEventId)userObject.Data!.Id)
                {
                    case LiveEventId.AcceptName:
                        _logger?.LogDebug("Name has been accepted");
                        break;
                    case LiveEventId.QuizStart:
                        _logger?.LogInformation("The quiz has started");

                        await QuizStarted.InvokeEventAsync(this, new() { 
                            QuizTitle = JsonSerializer.Deserialize(content.Span, QuizInfoContext.Default.QuizInfo).Title 
                        });

                        break;
                    case LiveEventId.QuizEnd: // kicked or left
                        await LeaveAsync(LeaveCondition.Kicked);
                        break;
                    case LiveEventId.TimeUp:
                        Debug.WriteLine("Question to respond to has timed out");
                        _logger?.LogDebug("Question has timed out");
                        break;
                    case LiveEventId.QuestionStart:
                        var question = JsonSerializer.Deserialize(content.Span, QuizQuestionContext.Default.QuizQuestion);

                        Debug.Assert(question != null); // question should not be null;
                        Debug.Assert(question.Data != null);

                        question.Info = JsonSerializer.Deserialize(question.Data.Content, QuizQuestionDataContext.Default.QuizQuestionData);

                        Debug.Assert(question.Info != null);
                        
                        question.Info.QuestionType = Types.Question.GetQuestionType(question.Info.Type);

                        await QuestionReceived.InvokeEventAsync(this, new() { Question = question.Info });

                        break;
                    case LiveEventId.QuestionEnd: // question has ended
                        break;

                    case LiveEventId.RequestFeedback: // the host has asked for feedback
                        await Task.Delay(500); // slight delay to be able to send feedback
                        await FeedbackRequested.InvokeEventAsync(this, EventArgs.Empty); // this event has no specific data but we include EventArgs for consistancy
                        break;
                }
                break;
        }
    }
}
