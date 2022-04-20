using System.Text.Json.Serialization.Metadata;

namespace Kahoot.NET.Client;

internal class InternalSerializer
{
    internal static Memory<byte> Serialize<TData>(TData data, JsonTypeInfo<TData> typeInfo, out ReadOnlySpan<char> json)
    {
        json = JsonSerializer.Serialize(data, typeInfo);
        return Encoding.UTF8.GetBytes(json.ToString());
    }
    internal static Memory<byte> Serialize<TData>(TData data, out ReadOnlySpan<char> json)
    {
        json = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(json.ToString());
    }
}
