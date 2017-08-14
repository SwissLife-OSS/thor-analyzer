using System.Collections.Generic;

namespace ChilliCream.Logging.Analyzer.Rules
{
    /// <summary>
    /// A rule set which contains vital rules. Those rules are defined by Microsoft.
    /// </summary>
    public class BasicRuleSet
        : IRuleSet
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="BasicRuleSet"/> class.
        /// </summary>
        public BasicRuleSet()
        {
            Rules = new[]
            {
                new DuplicateEventIdsNotAllowed(this)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<IRule> Rules { get; }
    }
}