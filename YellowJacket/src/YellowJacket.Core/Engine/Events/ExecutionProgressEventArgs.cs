using System;

namespace YellowJacket.Core.Engine.Events
{
    /// <summary>
    /// Contains the arguments for the Execution Progress event.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class ExecutionProgressEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the progress.
        /// </summary>
        /// <value>
        /// The progress.
        /// </value>
        public decimal Progress { get; }

        public string CurrentState { get;  }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionProgressEventArgs"/> class.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="currentState">The current state.</param>
        public ExecutionProgressEventArgs(decimal progress, string currentState)
        {
            Progress = progress;
            CurrentState = currentState;
        }

        #endregion
    }
}
