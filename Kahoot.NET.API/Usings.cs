global using Kahoot.NET.API.Shared;
global using Kahoot.NET.Parsers;
global using Kahoot.NET.API.Requests;
global using Kahoot.NET.API.Responses;
global using Kahoot.NET.API.Requests.Login;
global using Kahoot.NET.API.Requests.Handshake;

#if NET6_0_OR_GREATER
global using Kahoot.NET.API.Json;
#endif

global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;


global using System.Diagnostics;
global using System.Net.WebSockets;
global using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Kahoot.NET")]
[assembly: InternalsVisibleTo("Kahoot.NET.Hosting")]
[assembly: InternalsVisibleTo("Kahoot.NET.Tests")]
[assembly: InternalsVisibleTo("Kahoot.NET.Benchmarks")]
