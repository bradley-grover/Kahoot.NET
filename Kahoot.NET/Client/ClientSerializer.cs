namespace Kahoot.NET.Client;

internal static class ClientSerializer
{
    /// <summary>
    /// Serializes the data passed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    internal static ArraySegment<byte> Serialize<T>(T data, out ReadOnlySpan<char> json)
    {
        json = JsonSerializer.Serialize(data);
        return new ArraySegment<byte>(Encoding.UTF8.GetBytes(json.ToString()));
    }
    internal static ArraySegment<byte> Serialize<T>(T data)
    {
        return new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
    }
}
