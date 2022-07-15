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
