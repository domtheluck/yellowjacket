﻿// ***********************************************************************
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

using System.Collections.Generic;
using System.IO;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Infrastructure
{
    // TODO: Need to add some logic to this logger to support intelligent file/folder management since it will probably be the default one.
    
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FileLogger(string path)
        {
            _path = path;
        }

        #endregion
    }
}