using YellowJacket.Core.Enums;

namespace YellowJacket.Core.Framework
{
    /// <summary>
    /// Contains the execution options.
    /// </summary>
    class Configuration
    {
        #region Properties

        public BrowserType Browser { get; internal set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        internal Configuration()
        {

        }

        #endregion
    }
}
