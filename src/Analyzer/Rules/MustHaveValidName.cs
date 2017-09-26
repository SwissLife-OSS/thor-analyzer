using System;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// A rule which verifies if the event source has a valid name.
    /// </summary>
    public class MustHaveValidName
        : IEventSourceRule
    {
        private static Regex _namePattern = new Regex(@"^[a-zA-Z]+(\-[a-zA-Z]+)*$",
            RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Initiates a new instance of the <see cref="MustHaveValidName"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public MustHaveValidName(IRuleSet ruleSet)
        {
            if (ruleSet == null)
            {
                throw new ArgumentNullException(nameof(ruleSet));
            }

            RuleSet = ruleSet;
        }

        /// <inheritdoc/>
        public IRuleSet RuleSet { get; }

        /// <inheritdoc/>
        public IResult Apply(EventSourceSchema schema, EventSource eventSource)
        {
            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            if (!_namePattern.IsMatch(schema.ProviderName))
            {
                return new Error(this, "The EventSource must have a valid name.");
            }

            return new Success(this);
        }
    }
}