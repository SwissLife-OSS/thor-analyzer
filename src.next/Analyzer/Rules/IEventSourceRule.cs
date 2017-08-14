using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>An <see cref="IEventSourceRule"/> will be executed once per <see cref="EventSource"/>.</remarks>
    public interface IEventSourceRule
        : IRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="eventSource"></param>
        /// <returns></returns>
        IResult Apply(EventSourceSchema schema, EventSource eventSource);
    }
}