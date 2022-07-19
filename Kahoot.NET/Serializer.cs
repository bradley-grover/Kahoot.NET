using System.Text.Json.Serialization.Metadata;

namespace Kahoot.NET;

/// <summary>
/// Handles serializing data into json byte buffers
/// </summary>
internal static class Serializer
{
    internal static JsonSerializerOptions Options { get; } = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Serializes an object of <typeparamref name="T"/> using the specified type info from a source generator
    /// </summary>
    /// <typeparam name="T">The type of data model to serialize</typeparam>
    /// <param name="data">The data to serialize</param>
    /// <param name="typeInfo">The type info from a source generator</param>
    /// <param name="json">JSON to log for debugging</param>
    /// <returns>Byte buffer</returns>
    internal static Memory<byte> Serialize<T>(T data, JsonTypeInfo<T> typeInfo, out ReadOnlySpan<char> json)
    {
        json = JsonSerializer.Serialize(data, typeInfo);

        return Encoding.UTF8.GetBytes(json.ToString());
    }

    /// <summary>
    /// Serializes an object of <typeparamref name="T"/> using the specified type info from a source generator
    /// </summary>
    /// <typeparam name="T">The type of data model to serialize</typeparam>
    /// <param name="data">The data to serialize</param>
    /// <param name="json">JSON to log for debugging</param>
    /// <returns>Byte buffer</returns>
    internal static Memory<byte> Serialize<T>(T data, out ReadOnlySpan<char> json)
    {
        json = JsonSerializer.Serialize(data, Options);

        return Encoding.UTF8.GetBytes(json.ToString());
    }
}
