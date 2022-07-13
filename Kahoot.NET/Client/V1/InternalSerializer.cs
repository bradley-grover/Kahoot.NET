using System.Text.Json.Serialization.Metadata;

namespace Kahoot.NET.Client;

/// <summary>
/// Internal serializer for message sending
/// </summary>
internal static class InternalSerializer
{
    /// <summary>
    /// Options for the JSON serializer
    /// </summary>
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Serializes an object using the JSON type info and returns a byte array of it as well as raw json sent
    /// </summary>
    /// <typeparam name="TData">The class object type to serialize</typeparam>
    /// <param name="data">The data to serialize</param>
    /// <param name="typeInfo">The type info used for serialization</param>
    /// <param name="json">The json sent</param>
    /// <returns>Raw data to send to the client</returns>
    internal static Memory<byte> Serialize<TData>(TData data, JsonTypeInfo<TData> typeInfo, out ReadOnlySpan<char> json)
    {
        json = JsonSerializer.Serialize(data, typeInfo);
        return Encoding.UTF8.GetBytes(json.ToString());
    }

    /// <summary>
    /// Serializing a object and returns a byte array of it as well as the raw json sent
    /// </summary>
    /// <typeparam name="TData">The class object type to serialize</typeparam>
    /// <param name="data">The data to serialize</param>
    /// <param name="json">The json data to be used for logging</param>
    /// <returns>Raw data to send to the client</returns>
    internal static Memory<byte> Serialize<TData>(TData data, out ReadOnlySpan<char> json)
    {
        json = JsonSerializer.Serialize(data, Options);
        return Encoding.UTF8.GetBytes(json.ToString());
    }
}
