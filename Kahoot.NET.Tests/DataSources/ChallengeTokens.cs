using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Kahoot.NET.Tests.DataSources;

public class ChallengeTokens : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var decoded = TestHelper.Configuration.GetSection("successfulJoins").Get<SuccessfulDecode[]>();

        if (decoded is null)
        {
            throw new Exception("Could not get decoded");
        }

        foreach (var item in decoded)
        {
            yield return new object[] { item.Header, item.Challenge, item.Expected };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
