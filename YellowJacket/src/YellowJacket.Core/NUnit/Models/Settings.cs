using System.Collections.Generic;
using System.Xml.Serialization;

namespace YellowJacket.Core.NUnit.Models
{
    /// <summary>
    /// Represents an NUnit list of settings. 
    /// </summary>
    [XmlRoot(ElementName = "settings")]
    public class Settings
    {
        /// <summary>
        /// Gets or sets the setting list.
        /// </summary>
        /// <value>
        /// The setting list.
        /// </value>
        [XmlElement(ElementName = "setting")]
        public List<Setting> SettingList { get; set; }
    }
}
