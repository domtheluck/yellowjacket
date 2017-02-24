using NUnit.Engine;

namespace YellowJacket.Core.NUnit
{
    public delegate void TestReportHandler(object sender, TestReportEventArgs eventArgs);

    /// <summary>
    /// Used to handle the process progress reports and other events from the NUnit test.
    /// </summary>
    public class CustomTestEventListener: ITestEventListener
    {
        #region Events

        public event TestReportHandler TestReport;

        #endregion

        #region Public Methods

        /// <summary>
        /// Event handler used for the NUnit progress report.
        /// </summary>
        /// <param name="report">The report.</param>
        public void OnTestEvent(string report)
        {
            TestReport?.Invoke(this, new TestReportEventArgs(report));
        }

        #endregion
    }
}
