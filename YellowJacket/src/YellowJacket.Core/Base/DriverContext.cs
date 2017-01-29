using OpenQA.Selenium;

namespace YellowJacket.Core.Base
{
    /// <summary>
    /// The driver context.
    /// </summary>
    public static class DriverContext
    {
        #region Properties

        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        /// <value>
        /// The driver.
        /// </value>
        public static IWebDriver Driver   { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        /// <value>
        /// The browser.
        /// </value>
        public static Browser Browser { get; set; }

        #endregion
    }
}
