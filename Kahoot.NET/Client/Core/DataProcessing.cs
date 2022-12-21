/*
 * Uncomment the preprocessor symbol below to show the text representation of the json during execution
 * removed in release for performance reasons
 * useful for debug cases
 */

#if DEBUG
//#define VIEWTEXT
#endif

// file contains method for processing data from the ReceiveAsync method and performing the required action per channel or message id

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    // method bumps up the id and ack number for each request
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Bump()
    {
        Interlocked.Increment(ref _stateObject.id);
        Interlocked.Increment(ref _stateObject.ack);
    }

    internal Task ProcessAsync(ReadOnlyMemory<byte> data)
    {
        data = data[1..^1]; // remove first character '[' and remove ']' at end so we don't have to serialize to an array, this format is guranteed

        ReadOnlySpan<byte> byteData = data.Span; // store span instance to avoid cost of recreation

        Message? message = JsonSerializer.Deserialize(byteData, MessageContext.Default.Message); // deserialize using the UTF-8 bytes and using source generators

#if VIEWTEXT
        WriteToLogger(byteData);
#endif

        if (message is null) // this case shoudn't ever happen
        {
            Debug.WriteLine("Message received as null - investigate");
            return Task.CompletedTask;
        }

        // log incoming channel and message id
        _logger?.LogDebug("[RECEIVED]:\n[ID]: {messageId}\n[CHANNEL]: {channel}", message.Id ?? "Unknown", message.Channel);

        return message.Id == null ? 
            ProcessChannelAsync(data, message.Channel, message?.Data?.Type) :
            ProcessTaggedChannelAsync(data, uint.Parse(message.Id.AsSpan()), message.Channel, message?.Data?.Type);

    }

#if VIEWTEXT
    internal void WriteToLogger(ReadOnlySpan<byte> data) // try to save out on performance hit during testing by pooling
    {
        int charsRequired = Encoding.UTF8.GetCharCount(data);

        char[] chars = ArrayPool<char>.Shared.Rent(charsRequired);

        try
        {
            int written = Encoding.UTF8.GetChars(data, chars);

            _logger?.LogInformation("[RECEIVED]: {json}", new string(chars.AsSpan(0, written)));
        }
        finally
        {
            ArrayPool<char>.Shared.Return(chars);
        }
    }
#endif
}
