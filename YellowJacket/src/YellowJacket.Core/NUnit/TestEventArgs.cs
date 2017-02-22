using System;

namespace YellowJacket.Core.NUnit
{
    /// <summary>
    /// Contains the arguments for the test event.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class TestEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the report.
        /// </summary>
        /// <value>
        /// The report.
        /// </value>
        public string Report { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestEventArgs"/> class.
        /// </summary>
        /// <param name="report">The report.</param>
        public TestEventArgs(string report)
        {
            Report = report;
        }

        #endregion
    }
}
