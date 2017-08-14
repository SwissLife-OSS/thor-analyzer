using System;

namespace ChilliCream.Logging.Analyzer.Rules
{
    /// <summary>
    /// A result that represents a success status for a rule.
    /// </summary>
    public sealed class Success
        : IResult
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="Success"/> class.
        /// </summary>
        /// <param name="rule">A correlated rule.</param>
        public Success(IRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }

            Rule = rule;
        }

        /// <inheritdoc/>
        public IRule Rule { get; }
    }
}