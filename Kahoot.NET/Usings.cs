global using Kahoot.NET.API; // general api
global using Kahoot.NET.Client.Events; // client events
global using Kahoot.NET.API.Responses; // server responses
global using Kahoot.NET.API.Requests; // client requests
global using Kahoot.NET.API.Shared; // shared api of Kahoot!
global using Kahoot.NET.API.Authentication; // authentication for creation of websocket key
global using Kahoot.NET.API.Json; // source generators for better performance
global using Kahoot.NET.Extensions; // extensions for performance or ease of access
global using Kahoot.NET.API.Requests.Login;
global using Kahoot.NET.API.Requests.Handshake;
global using Kahoot.NET.RandomUserAgent; // user agent generation
global using Kahoot.NET.Mathematics; // expression parser for like "(5+5)*2"

global using System.Diagnostics; // debug asserts
global using System.Buffers; // ArrayPool<T> support
global using System.Numerics; // Vector<T> for hardware acceleration
global using System.Runtime.CompilerServices; // Unsafe, MethodImpl, Internals
global using System.Runtime.InteropServices; // MemoryMarshal for span casting etc
global using System.Net.WebSockets; // ClientWebSocket
global using System.Diagnostics.CodeAnalysis; // Nullability analysis
global using System.Text; // encodings for bytes
global using System.Text.Json; // json support
global using System.Text.Json.Serialization; // json further support
global using System.Text.Json.Serialization.Metadata; // json metadata support
global using Microsoft.Extensions.Logging; // only external depedency (for logging kahoot! events)
