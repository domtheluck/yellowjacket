using System;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Logging
{
    /// <summary>
    /// Used to write log to a file.
    /// </summary>
    /// <seealso cref="ILogger" />
    [Serializable]
    internal class FileLogger: ILogger
    {
        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLine(string value)
        {
            
        }

        /// <summary>
        /// Writes the specified value to the log.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void Write(string value)
        {

        }
    }
}
