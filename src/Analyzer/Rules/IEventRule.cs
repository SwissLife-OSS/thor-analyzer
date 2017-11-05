#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace Thor.Analyzer.Rules
{
    /// <summary>
    /// Describes an event rule.
    /// </summary>
    /// <remarks>
    /// A <see cref="IEventRule"/> will be executed for every event of a <see cref="EventSource"/>.
    /// </remarks>
    public interface IEventRule
        : IRule
    {
        /// <summary>
        /// Applies the rule on the event schema.
        /// </summary>
        /// <param name="schema">A event schema.</param>
        /// <param name="eventSource">A correlated event provider.</param>
        /// <returns>A result.</returns>
        IResult Apply(EventSchema schema, EventSource eventSource);
    }
}