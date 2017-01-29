using OpenQA.Selenium.Support.PageObjects;

namespace YellowJacket.Core.Base
{
    /// <summary>
    /// Base page.
    /// </summary>
    public abstract class BasePage: Base
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        protected BasePage()
        {
            PageFactory.InitElements(DriverContext.Driver, this);
        }

        #endregion
    }
}
