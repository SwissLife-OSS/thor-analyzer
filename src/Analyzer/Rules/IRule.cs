namespace Thor.Analyzer.Rules
{
    /// <summary>
    /// Describes a rule.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Gets the correlated rule set.
        /// </summary>
        IRuleSet RuleSet { get; }
    }
}