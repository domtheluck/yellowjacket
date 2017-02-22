using NUnit.Engine;

namespace YellowJacket.Core.NUnit
{
    public delegate void TestEventHandler(object sender, TestEventArgs eventArgs);

    /// <summary>
    /// Used to handle the process progress reports and other events from the NUnit test.
    /// </summary>
    public class CustomTestEventListener: ITestEventListener
    {
        #region Events

        public event TestEventHandler OnTest;

        #endregion

        #region Public Methods

        /// <summary>
        /// Event handler used for the NUnit progress report.
        /// </summary>
        /// <param name="report">The report.</param>
        public void OnTestEvent(string report)
        {
            OnTest?.Invoke(this, new TestEventArgs(report));
        }

        #endregion
    }
}
