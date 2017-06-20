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

using OpenQA.Selenium;
using YellowJacket.Core.Enums;

namespace YellowJacket.Core.Framework
{
    /// <summary>
    /// Contains browser and driver implementation.
    /// </summary>
    public class Browser
    {
        #region Private Members

        public readonly IWebDriver Driver;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public Browser(IWebDriver driver)
        {
            Driver = driver;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the browser type.
        /// </summary>
        /// <value>
        /// The browser type.
        /// </value>
        public BrowserType Type { get; set; }

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
