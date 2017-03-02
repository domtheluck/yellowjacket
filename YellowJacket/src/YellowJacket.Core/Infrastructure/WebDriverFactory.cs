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

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using YellowJacket.Core.Enums;

namespace YellowJacket.Core.Infrastructure
{
    /// <summary>
    /// Used to create Web Driver.
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
