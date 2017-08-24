using System.Collections.Generic;

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// Describes a rule set.
    /// </summary>
    public interface IRuleSet
    {
        /// <summary>
        /// Gets the correlated rules.
        /// </summary>
        IEnumerable<IRule> Rules { get; }
    }
}