using System.Collections.Generic;

namespace ChilliCream.Logging.Analyzer.Rules
{
    /// <summary>
    /// A rule set which contains vital rules. Those rules are defined by ChilliCream.
    /// </summary>
    public class AdvancedRuleSet
        : IRuleSet
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="AdvancedRuleSet"/> class.
        /// </summary>
        public AdvancedRuleSet()
        {
            Rules = new[]
            {
                new MustHaveAStaticLogProperty(this)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<IRule> Rules { get; }
    }
}