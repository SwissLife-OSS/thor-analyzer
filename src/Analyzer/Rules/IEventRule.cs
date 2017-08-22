using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>A <see cref="IEventRule"/> will be executed for every event of a <see cref="EventSource"/>.</remarks>
    public interface IEventRule
        : IRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="eventSource"></param>
        /// <returns></returns>
        IResult Apply(EventSchema schema, EventSource eventSource);
    }
}