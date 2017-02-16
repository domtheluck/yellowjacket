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
            get
            {
                return (BasePage)ScenarioContext.Current["currentPage"];
            }
            set
            {
                ScenarioContext.Current["currentPage"] = value;
            }
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

        }

        #endregion
    }
}
