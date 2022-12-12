namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task ServiceAsync(ReadOnlyMemory<byte> jsonContent, string dataType)
    {
        switch (dataType)
        {
            case Types.LoginResponse:
                var error = JsonSerializer.Deserialize<Message<DataErrorResponse>>(jsonContent.Span)!;

                // we know the data can't be null cause a login response was passed

                if (error.Data!.Error != null)
                {
                    switch (error.Data.Error)
                    {
                        case Types.Errors.UserInput:
                            await Joined.InvokeEventAsync(this, new()
                            {
                                Success = false,
                                Error = Data.Errors.JoinErrors.DuplicateUserName
                            });
                            break;
                    }
                }
                

                await SendLastLoginMessageAsync();
                break;
        }
    }
}
