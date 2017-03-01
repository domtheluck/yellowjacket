using System;
using System.Collections.Generic;
using System.IO;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Logging
{
    /// <summary>
    /// Used to write log to a file.
    /// </summary>
    /// <seealso cref="ILogger" />
    internal class FileLogger: ILogger
    {
        #region Private Members

        private readonly string _path;

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public void WriteLine(string content)
        {
            File.AppendAllLines(_path, new List<string> { content });
        }

        /// <summary>
        /// Writes the specified content to the log.
        /// </summary>
        /// <param name="content">The content to write.</param>
        public void Write(string content)
        {
            File.AppendAllText(_path, content);
        }

        #endregion

        #region Constructors

        public FileLogger(string path)
        {
            _path = path;
        }

        #endregion
    }
}
