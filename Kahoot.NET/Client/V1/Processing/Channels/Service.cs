using Kahoot.NET.API.Shared;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task ServiceAsync(string dataType)
    {
        switch (dataType)
        {
            case Types.LoginResponse:
                await SendLastLoginMessageAsync();
                break;
        }
    }
}
