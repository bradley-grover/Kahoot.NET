using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Kahoot.NET.API.Authentication.Token;

namespace Kahoot.NET.Benchmarks;

internal static class Mock
{
    internal static class Auth
    {
        public static string Create(ReadOnlySpan<char> sessionHeaderToken, ReadOnlySpan<char> challengeFunction)
        {
            var header = Header.Create(sessionHeaderToken);
            var challenge = Challenge.CreateToken(challengeFunction);

            ref char headerRef = ref MemoryMarshal.GetReference(header);
            ref char challengeRef = ref MemoryMarshal.GetReference(challenge);

            // new span to allocate for the token
            Span<char> merged = stackalloc char[header.Length];

            ref char mergedRef = ref MemoryMarshal.GetReference(merged);

            for (var i = 0; i < header.Length; i++) // loop every character in the header
            {
                // black magic stuff
                var character = (int)Unsafe.Add(ref headerRef, i);

                var mod = (int)Unsafe.Add(ref challengeRef, i % challenge.Length);

                var decoded = character ^ mod;

                Unsafe.Add(ref mergedRef, i) = Convert.ToChar(decoded);
            }

            return new string(merged); // allocate into a string
        }
    }
}
