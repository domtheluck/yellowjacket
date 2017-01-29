namespace YellowJacket.Core.Base
{
    /// <summary>
    /// Base step class.
    /// </summary>
    public abstract class BaseStep: Base
    {
        #region Public Methods

        /// <summary>
        /// Navigates to the site.
        /// </summary>
        public override void NavigateSite()
        {
            // TODO: We need to navigate to the site homepage. Should be in configuration somehow.
            //DriverContext.Browser.GoToUrl(EnvironmentHelper.GetLoginUrl(Settings.Environment));
        }

        #endregion
    }
}
