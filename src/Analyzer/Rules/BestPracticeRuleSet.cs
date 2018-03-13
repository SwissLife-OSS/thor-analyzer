using System.Collections.Generic;

namespace Thor.Analyzer.Rules
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
                new MustHaveStaticLogFieldOrProperty(this),
                new MustHaveValidName(this)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<IRule> Rules { get; }
    }
}