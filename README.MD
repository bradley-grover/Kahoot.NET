# Kahoot.NET - .NET version of https://www.npmjs.com/package/kahoot.js-updated

![GitHub](https://img.shields.io/github/license/ac111897/Kahoot.NET)
![GitHub last commit](https://img.shields.io/github/last-commit/ac111897/Kahoot.NET)
![GitHub repo size](https://img.shields.io/github/repo-size/ac111897/Kahoot.NET)
![Lines of code](https://img.shields.io/tokei/lines/github/ac111897/Kahoot.NET)

## Overview
Kahoot.NET C# Library to connect to Kahoot! games and interact as a player

Connects to game using a WebSocket and is an event based client, so make sure to have it hosted as a background service or something like this at the end of your app
```cs
Console.ReadLine(); // at the end
// or perhaps
using System.Threading.Tasks;

// after making client and binding events
await Task.Delay(-1);
```

## Project Progress

- [x] Generate token to connect
- [x] Create handshake with Kahoot websocket

- [x] Join the game with a name (event not yet supported)
- [ ] Support answering
- [ ] Support various client events

## Examples 

### Demo Console App
```cs
using Kahoot.NET;
using Kahoot.NET.Client;
using Kahoot.NET.Exceptions;

namespace SomeApp;

public class Program 
{
   public static async Task Main(string[] args)
   {
       IKahootClient client = Kahoot.CreateClient();
       
       // also bind other events later but for now we can 
       client.OnJoined += Client_OnJoined;


       int code = int.Parse(Console.ReadLine());
       // wrap in try catch, if the code doesn't exist it throws a GameNotFoundException
       try 
       {
           await client.JoinAsync(code, "somename");
       }
       catch (GameNotFoundException) 
       {
           return;
       }
       // keep main thread alive whilst our client receives data from the websocket in a background thread
       await Task.Delay(-1);
       
   }
   public static async Task Client_OnJoined(object? sender, EventArgs e)
   {
        Console.WriteLine("I have joined the kahoot");
   }
}

```
