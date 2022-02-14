using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahoot.NET.Internals.Messages.Handshake.Advice;

/// <summary>
/// Advice for server connection timings
/// </summary>
public class ClientAdvice : BaseAdvice
{
    /// <summary>
    /// Default options for <see cref="Advice"/>
    /// </summary>
    public static ClientAdvice Default { get; } = new ClientAdvice() { Interval = 0, TimeOut = 60_000 };
}
