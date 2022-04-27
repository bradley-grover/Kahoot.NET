using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Responses.Login;

internal class LoginResponse
{
    [JsonPropertyName("channel")]
    public string Channel { get; set; }

    [JsonPropertyName("data")]
    public LoginResponseData Data { get; set; }

    [JsonPropertyName("ext")]
    public ExtWithTimetrack Ext { get; set; }
}
