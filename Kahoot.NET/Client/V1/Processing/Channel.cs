using Kahoot.NET.API;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ProcessChannelAsync(string content, string channel, string? data = null)
    {
        switch (channel)
        {
            case Channels.Service:
                if (data is null) return;

                await ServiceAsync(data);
                break;
        }
    }
}
