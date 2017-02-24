using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using YellowJacket.Core.Enums;

namespace YellowJacket.Core.Framework
{
    /// <summary>
    /// Uses to create Web Driver.
    /// </summary>
    internal class WebDriverFactory
    {
        /// <summary>
        /// Gets a <see cref="IWebDriver"/> according to the specified browser.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <returns><see cref="IWebDriver"/>.</returns>
        public static IWebDriver Get(BrowserType browser)
        {
            IWebDriver webDriver = null;

            // TODO: Check if we need to modify the factory to support local as well as portable browser
            switch (browser)
            {
                case BrowserType.InternetExplorer32:
                case BrowserType.InternetExplorer64:
                    InternetExplorerOptions options = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Ignore,
                        EnableNativeEvents = false,
                        IgnoreZoomLevel = true
                    };

                    webDriver = new InternetExplorerDriver(options);

                    break;

                case BrowserType.Firefox:
                    break;

                case BrowserType.Chrome:
                    break;

                case BrowserType.Edge:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }

            return webDriver;
        }
    }
}
