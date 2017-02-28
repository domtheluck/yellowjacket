using System;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Logging
{
    /// <summary>
    /// Used to write log to the console.
    /// </summary>
    /// <seealso cref="ILogger" />
    [Serializable]
    internal class ConsoleLogger: ILogger
    {
        #region Public Methods

        /// <summary>
        /// Writes the specified line to the log.
        /// </summary>
        /// <param name="line">The line to write.</param>
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        /// <summary>
        /// Writes the specified value to the log.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void Write(string value)
        {
            Console.Write(value);
        }

        #endregion
    }
}
