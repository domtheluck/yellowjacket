// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YellowJacket.Core.Plugins.Interfaces;

namespace YellowJacket.Core.Plugins
{
    /// <inheritdoc />
    /// <summary>
    /// Used to write log to a file.
    /// </summary>
    /// <seealso cref="T:YellowJacket.Core.Plugins.Interfaces.ILogPlugin" />
    internal class BasicFileLogPlugin : ILogPlugin
    {
        #region Private Members

        private readonly string _filename;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicFileLogPlugin"/> class.
        /// </summary>
        /// <param name="path">The path where to create log.</param>
        public BasicFileLogPlugin(string path)
        {
            _filename = Path.Combine(path, $"yellowjacket_{DateTime.Now:yyyyMMddHHmmssffffff}.log");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Writes the value content to the log.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Write(string value)
        {
            File.AppendAllText(_filename, value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Writes the specified line to the log.
        /// </summary>
        /// <param name="line">The line to write.</param>
        public void WriteLine(string line)
        {
            WriteAllLine(new List<string>{line});
        }

        /// <inheritdoc />
        /// <summary>
        /// Writes the specified lines to the log.
        /// </summary>
        /// <param name="lines">The lines to write.</param>
        public void WriteAllLine(IEnumerable<string> lines)
        {
            File.AppendAllLines(_filename, lines.Select(x => $"{GetLinePrefix()} {x}").ToList());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the line prefix.
        /// </summary>
        /// <returns>The line prefix.</returns>
        private string GetLinePrefix()
        {
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        }

        #endregion
    }
}
