using System;

namespace YellowJacket.Core.Engine
{
    /// <summary>
    /// Contains the arguments for the Execution Stop event.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class ExecutionStopEventArgs: EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionStopEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ExecutionStopEventArgs(Exception exception)
        {
            Exception = exception;
        }

        #endregion
    }
}
