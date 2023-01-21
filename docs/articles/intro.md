# Getting started with Kahoot.NET

Kahoot.NET is a async library over the Kahoot! game. It also relies heavily on the event pattern as a way of notifying players.

Responding an event is the basis of this library, to interact with the Kahoot! game.

To start off, let's create a console app so we can use Kahoot.NET. Kahoot.NET supports .NET 6+ so any project with that version or higher is supported.

Assuming we name the console app like 'KahootPlay', we should end up with something like this:

[!code-csharp[StaticVoidMain](samples/program.cs)]

We'd like to modify this so that we can use async. So let's change that to this:

[!code-csharp[AsyncMain](samples/async_main.cs)]
