using OpenQA.Selenium;
using YellowJacket.Core.Enums;

namespace YellowJacket.Core.Base
{
    /// <summary>
    /// Contains browser and driver implementation.
    /// </summary>
    public class Browser
    {
        #region Private Members

        public readonly IWebDriver _driver;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public Browser(IWebDriver driver)
        {
            _driver = driver;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the browser type.
        /// </summary>
        /// <value>
        /// The browser type.
        /// </value>
        public BrowserType type { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Goes to URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void GoToUrl(string url)
        {
            DriverContext.Driver.Url = url;
        }

        #endregion
    }
}
