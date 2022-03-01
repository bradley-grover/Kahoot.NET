namespace Kahoot.NET.Client;

internal class InternalSerializer
{
    internal static Memory<byte> Serialize<TData>(TData data, out ReadOnlySpan<char> json)
    {
        json = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(json.ToString());
    }
}
