using System.ComponentModel;

namespace YellowJacket.Core.Enums
{
    /// <summary>
    /// Contains the browser type.
    /// </summary>
    public enum BrowserType
    {
        [Description("InternetExplorer32")]
        InternetExplorer32 = 0,

        [Description("InternetExplorer64")]
        InternetExplorer64 = 1,

        [Description("Firefox")]
        Firefox = 2,

        [Description("Chrome")]
        Chrome = 3
    }
}
