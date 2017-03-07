using System.ComponentModel;

namespace YellowJacket.Core.Enums
{
    /// <summary>
    /// Contains the browser type.
    /// </summary>
    public enum BrowserType
    {
        [Description("None")]
        None,

        [Description("InternetExplorer32")]
        InternetExplorer32,

        [Description("InternetExplorer64")]
        InternetExplorer64,

        [Description("Firefox")]
        Firefox,

        [Description("Chrome")]
        Chrome,

        [Description("Edge")]
        Edge
    }
}
