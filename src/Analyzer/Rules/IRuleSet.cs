using System.Collections.Generic;

namespace Thor.Analyzer.Rules
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