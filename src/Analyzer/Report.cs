using Thor.Analyzer.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Thor.Analyzer
{
    /// <summary>
    /// A report which summarizes the rules.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        /// <param name="providerName">An event provider name.</param>
        /// <param name="ruleSets">A collection of rule sets.</param>
        /// <param name="results">A collection of results.</param>
        public Report(string providerName, IEnumerable<IRuleSet> ruleSets,
            IEnumerable<IResult> results)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException(nameof(providerName));
            }
            if (ruleSets == null)
            {
                throw new ArgumentNullException(nameof(ruleSets));
            }
            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            Errors = results.OfType<Error>().ToArray();
            HasErrors = Errors.Any();
            RuleSets = ruleSets.ToArray();
            ProviderName = providerName;
        }

        /// <summary>
        /// Gets a collection of errors.
        /// </summary>
        public IReadOnlyCollection<Error> Errors { get; }

        /// <summary>
        /// Gets a value which indicates whether this report has any errors.
        /// </summary>
        public bool HasErrors { get; }

        /// <summary>
        /// Get the name for the provider which has been analyzed.
        /// </summary>
        public string ProviderName { get; }

        /// <summary>
        /// Gets a collection of applied rule sets.
        /// </summary>
        public IReadOnlyCollection<IRuleSet> RuleSets { get; }
    }
}