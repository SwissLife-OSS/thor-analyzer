using System;
using System.Collections.Generic;

namespace Thor.Analyzer.Rules
{
    /// <summary>
    /// A result that represents an error status for a rule.
    /// </summary>
    public class Error
        : IResult
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="rule">A correlated rule.</param>
        /// <param name="reason">A message that explains why this error happens.</param>
        public Error(IRule rule, string reason)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentNullException(nameof(reason));
            }

            Rule = rule;
            Reason = reason;
        }

        /// <summary>
        /// Initiates a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="rule">A correlated rule.</param>
        /// <param name="reason">A message that explains why this error happens.</param>
        /// <param name="details">A collection of error details.</param>
        public Error(IRule rule, string reason, IReadOnlyCollection<string> details)
            : this(rule, reason)
        {
            if (details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }

            Details = details;
        }

        /// <inheritdoc/>
        public IRule Rule { get; }

        /// <summary>
        /// Gets detailed explanations for this error.
        /// </summary>
        public IReadOnlyCollection<string> Details { get; }

        /// <summary>
        /// Gets an explanation for this error.
        /// </summary>
        public string Reason { get; }
    }
}