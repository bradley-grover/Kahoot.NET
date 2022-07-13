namespace Kahoot.NET.API.Authentication.Json;

/// <summary>
/// JSON source generator for <see cref="Responses.SessionResponse"/> for more effecient JSON Serializations/Deserializations
/// </summary>
[JsonSerializable(typeof(SessionResponse))]
internal partial class SessionContext : JsonSerializerContext
{

}
