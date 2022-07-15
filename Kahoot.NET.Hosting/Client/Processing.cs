using System.Text;
using Kahoot.NET.API.Shared.Json;

namespace Kahoot.NET.Hosting.Client;

public partial class KahootHost
{
    internal async Task ProcessDataAsync(Memory<byte> data)
    {
        string json = Encoding.UTF8.GetString(data.Span).AsSpan().RemoveBrackets();

        Logger?.LogDebug("[RECEIVE]: {json}", json);

        Message? message = JsonSerializer.Deserialize(json, MessageContext.Default.Message);

        if (message is null)
        {
            return;
        }

        
    }
}
