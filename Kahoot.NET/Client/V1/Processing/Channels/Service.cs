using Kahoot.NET.API.Shared;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task ServiceAsync(string jsonContent, string dataType)
    {
        switch (dataType)
        {
            case Types.LoginResponse:
                JsonSerializer.Deserialize<DataErrorResponse>(jsonContent);

                await SendLastLoginMessageAsync();
                break;
        }
    }
}
