using System;

namespace YellowJacket.Core.Engine
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
        public double Progress { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionProgressEventArgs"/> class.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public ExecutionProgressEventArgs(double progress)
        {
            Progress = progress;
        }

        #endregion
    }
}
