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
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace YellowJacket.Core.Framework
{
    /// <summary>
    /// Base framework class.
    /// </summary>
    public class Base
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public BasePage CurrentPage
        {
            get => (BasePage)ScenarioContext.Current["currentPage"];
            set => ScenarioContext.Current["currentPage"] = value;
        }

        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        /// <value>
        /// The driver.
        /// </value>
        private IWebDriver Driver { get; set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets a instance of a page of type TPage.
        /// </summary>
        /// <typeparam name="TPage">The type of the page.</typeparam>
        /// <returns><see cref="TPage"/>.</returns>
        protected TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            TPage pageInstance = new TPage()
            {
                Driver = DriverContext.Driver
            };

            PageFactory.InitElements(DriverContext.Driver, this);

            return pageInstance;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes the driver.
        /// </summary>
        public static void CloseDriver()
        {
            if (DriverContext.Driver == null)
                return;

            DriverContext.Driver.Close();
            DriverContext.Driver.Dispose();
        }

        /// <summary>
        /// Cast the page as TPage type.
        /// </summary>
        /// <typeparam name="TPage">The type of the page.</typeparam>
        /// <returns><see cref="TPage"/>.</returns>
        public TPage As<TPage>() where TPage : BasePage
        {
            return (TPage)this;
        }

        /// <summary>
        /// Navigates to the site.
        /// </summary>
        public virtual void NavigateSite()
        {
            // TODO: This method is obsolete. Need to cleanup.
        }

        #endregion
    }
}
