namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// Descibes a result.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets the correlated rule.
        /// </summary>
        IRule Rule { get; }
    }
}