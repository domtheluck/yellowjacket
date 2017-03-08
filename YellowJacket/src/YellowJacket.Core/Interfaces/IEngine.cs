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

using System.Collections.Generic;
using YellowJacket.Core.Engine.Events;

namespace YellowJacket.Core.Interfaces
{
    public delegate void ExecutionStartHandler(object sender, ExecutionStartEventArgs eventArgs);
    public delegate void ExecutionStopHandler(object sender, ExecutionStopEventArgs eventArgs);
    public delegate void ExecutionCompletedHandler(object sender, ExecutionCompletedEventArgs eventArgs);
    public delegate void ExecutionProgressHandler(object sender, ExecutionProgressEventArgs eventArgs);

    /// <summary>
    /// Interface definition for the execution engine.
    /// </summary>
    public interface IEngine
    {
        #region Events

        event ExecutionStartHandler ExecutionStart;

        event ExecutionStopHandler ExecutionStop;

        event ExecutionCompletedHandler ExecutionCompleted;

        event ExecutionProgressHandler ExecutionProgress;

        #endregion

        #region Methods

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        void Execute(string assemblyPath, string feature);

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        /// <param name="browser">The browser</param>
        /// <param name="loggers">The loggers.</param>
        void Execute(string assemblyPath, string feature, List<ILogger> loggers);

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        /// <param name="browser">The browser</param>
        /// <param name="useLocalBrowser">Determines if we want to use the local browser or not.</param>
        void Execute(string assemblyPath, string feature, string browser, bool useLocalBrowser);

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        /// <param name="browser">The browser</param>
        /// <param name="useLocalBrowser">Determines if we want to use the local browser or not.</param>
        /// <param name="loggers">The loggers.</param>
        void Execute(string assemblyPath, string feature, string browser, bool useLocalBrowser, List<ILogger> loggers);

        #endregion
    }
}
