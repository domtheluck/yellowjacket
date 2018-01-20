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

using YellowJacket.Core.Engine; 
using YellowJacket.Core.Engine.EventArgs;
using YellowJacket.Core.Enums;

namespace YellowJacket.Core.Interfaces
{
    #region Delegates

    public delegate void ExecutionStartHandler(object sender, ExecutionStartEventArgs eventArgs);
    public delegate void ExecutionCompletedHandler(object sender, ExecutionCompletedEventArgs eventArgs);
    public delegate void ExecutionStopHandler(object sender, ExecutionStopEventArgs eventArgs);

    public delegate void ExecutionProgressHandler(object sender, ExecutionProgressEventArgs eventArgs);

    public delegate void FeatureExecutionProgressHandler(object sender, FeatureExecutionProgressEventArgs eventArgs);
    public delegate void ScenarioExecutionProgressHandler(object sender, ScenarioExecutionProgressEventArgs eventArgs);
    public delegate void StepExecutionProgressHandler(object sender, StepExecutionProgressEventArgs eventArgs);

    #endregion

    /// <summary>
    /// Interface definition for the execution engine.
    /// </summary>
    public interface IEngine
    {
        #region Events

        event ExecutionStartHandler ExecutionStart;
        event ExecutionCompletedHandler ExecutionCompleted;
        event ExecutionStopHandler ExecutionStop;

        event ExecutionProgressHandler ExecutionProgress;
        event FeatureExecutionProgressHandler FeatureExecutionProgress;
        event ScenarioExecutionProgressHandler ScenarioExecutionProgress;
        event StepExecutionProgressHandler StepExecutionProgress;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        EngineStatus Status { get; }

        /// <summary>
        /// Gets the run summary.
        /// </summary>
        /// <value>
        /// The run summary.
        /// </value>
        RunSummary RunSummary { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the specified configuration.
        /// </summary>
        /// <param name="engineConfiguration">The execution configuration.</param>
        void Run(Configuration engineConfiguration);

        #endregion
    }
}
