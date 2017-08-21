using System.Collections.Generic;

namespace ChilliCream.Logging.Analyzer.Rules
{
    /// <summary>
    /// A rule set which contains best practice rules.
    /// </summary>
    public class BestPracticeRuleSet
        : IRuleSet
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="BestPracticeRuleSet"/> class.
        /// </summary>
        public BestPracticeRuleSet()
        {
            Rules = new IRule[]
            {
                new MustHaveSinglePrivateConstructor(this),
                new MustHaveStaticLogProperty(this),
                new MustBeSealed(this)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<IRule> Rules { get; }
    }
}