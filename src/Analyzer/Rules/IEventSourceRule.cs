#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// Describes an event source rule.
    /// </summary>
    /// <remarks>
    /// An <see cref="IEventSourceRule"/> will be executed once per <see cref="EventSource"/>.
    /// </remarks>
    public interface IEventSourceRule
        : IRule
    {
        /// <summary>
        /// Applies the rule on the event source schema.
        /// </summary>
        /// <param name="schema">A event source schema.</param>
        /// <param name="eventSource">A correlated event provider.</param>
        /// <returns>A result.</returns>
        IResult Apply(EventSourceSchema schema, EventSource eventSource);
    }
}