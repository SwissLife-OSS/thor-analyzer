using System;
using System.Collections.Generic;

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// A result that represents a error status for a rule.
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
        public Error(IRule rule, string reason, IEnumerable<string> details)
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
        /// 
        /// </summary>
        public IEnumerable<string> Details { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Reason { get; }
    }
}