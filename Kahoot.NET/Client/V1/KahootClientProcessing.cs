#if DEBUG // also should only be done in DEBUG but that is up to you
//#define VIEW_TEXT_JSON // uncomment in front to view the json representation in text through logging instead of only the message content
#endif

using System.Buffers;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    // this method directly deserializes the bytes to json objects as it is faster than flat out converting to a string than deserializing
    // although for debugging it is very difficult to actually read what the data is by raw bytes so we convert it to a string when the symbol above is defined
    internal Task ProcessDataAsync(ReadOnlyMemory<byte> data)
    {
        data = data[1..^1]; // remove both [ ] because only one object is ever inside these arrays? why do they do this?

        ReadOnlySpan<byte> byteSpan = data.Span;

        Message? message = JsonSerializer.Deserialize(byteSpan, MessageContext.Default.Message);

        ViewJsonText(byteSpan);

        if (message is null)
        {
            return Task.CompletedTask;
        }

        Logger?.LogDebug("[RECEIVED]: {messageId} | {channel}", message.Id ?? "Unknown", message.Channel);

        return (message.Id is null ?
            ProcessChannelAsync(data, message.Channel, message?.Data?.Type) :
            ProcessChannelIdAsync(data, int.Parse(message.Id), message.Channel, message?.Data?.Type));
    }

    [Conditional("VIEW_TEXT_JSON")]
    internal void ViewJsonText(ReadOnlySpan<byte> data)
    {
        int charsRequired = Encoding.UTF8.GetCharCount(data);

        char[] pooledCharacters = ArrayPool<char>.Shared.Rent(charsRequired);

        try
        {
            int written = Encoding.UTF8.GetChars(data, pooledCharacters);

            Logger?.LogInformation("[RECEIVED]: {json}", new string(pooledCharacters.AsSpan(0, written)));
        }
        finally
        {
            ArrayPool<char>.Shared.Return(pooledCharacters);
        }
    }

    internal async Task<bool> AlertOnNull<T>(T value)
    {
        if (value is null)
        {
            await ClientError.InvokeEventAsync(this, new(new Exception("An error occured internally parsing message")));

            return true;
        }

        return false;
    }
}
