namespace YellowJacket.Core.Interfaces
{
    /// <summary>
    /// Interface definition for logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes the content to the log.
        /// </summary>
        /// <param name="content">The content to write.</param>
        void WriteLine(string content);

        /// <summary>
        /// Writes the specified content to the log.
        /// </summary>
        /// <param name="content">The content to write.</param>
        void Write(string content);
    }
}
