using System.Collections.Generic;

namespace ChilliCream.Tracing.Analyzer.Rules
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
                new MustBeSealed(this),
                new MustHaveSinglePrivateConstructor(this),
                new MustHaveStaticLogProperty(this),
                new MustHaveValidName(this)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<IRule> Rules { get; }
    }
}