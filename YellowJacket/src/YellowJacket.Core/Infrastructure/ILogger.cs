namespace YellowJacket.Core.Infrastructure
{
    /// <summary>
    /// Interface definition for logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes the specified line to the log.
        /// </summary>
        /// <param name="line">The line to write.</param>
        void WriteLine(string line);

        /// <summary>
        /// Writes the specified value to the log.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(string value);
    }
}
