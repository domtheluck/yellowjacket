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

namespace YellowJacket.Core.Engine.EventArgs
{
    /// <inheritdoc />
    /// <summary>
    /// Contains the arguments for the Scenario Execution Progress event.
    /// </summary>
    /// <seealso cref="T:System.EventArgs" />
    public class ScenarioExecutionProgressEventArgs : System.EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the execution percentage.
        /// </summary>
        /// <value>
        /// The execution percentage.
        /// </value>
        public double ExecutionPercentage { get; }

        #endregion

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:YellowJacket.Core.Engine.EventArgs.ScenarioExecutionProgressEventArgs" /> class.
        /// </summary>
        /// <param name="executionPercentage">The execution progress.</param>
        internal ScenarioExecutionProgressEventArgs(double executionPercentage) => ExecutionPercentage = executionPercentage;

        #endregion
    }
}
