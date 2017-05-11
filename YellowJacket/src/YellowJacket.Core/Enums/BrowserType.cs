using System.ComponentModel;

namespace YellowJacket.Core.Enums
{
    // TODO: We should think about a Java agent to be able to support more browser and OS like OS X and Linux.
    
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
