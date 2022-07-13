using System.Diagnostics.CodeAnalysis;
using Kahoot.NET.API.Shared;
using Kahoot.NET.API.Shared.Json;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ProcessDataAsync(Memory<byte> data)
    {
        string json = Encoding.UTF8.GetString(data.Span).AsSpan().RemoveBrackets();

        Logger?.LogDebug("[RECEIVED]: {json}", json);

        Message? message = JsonSerializer.Deserialize(json, MessageContext.Default.Message);

        if (message is null)
        {
            return;
        }

        await (message.Id is null ?
            ProcessChannelAsync(json, message.Channel, message?.Data?.Type) :
            ProcessChannelIdAsync(json, int.Parse(message.Id), message.Channel, message?.Data?.Type));
    }

    internal async Task<bool> AlertOnNull<T>(T value)
    {
        if (value is null)
        {
            if (ClientError is not null)
            {
                await ClientError.Invoke(this, new(new Exception("An error occured internally parsing message")));
            }

            return true;
        }

        return false;
    }
}
