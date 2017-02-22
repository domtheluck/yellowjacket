using System.Collections.Generic;
using System.Xml.Serialization;

namespace YellowJacket.Core.NUnit.Models
{
    /// <summary>
    /// Represents an NUnit list of properties. 
    /// </summary>
    [XmlRoot(ElementName = "properties")]
    public class Properties
    {
        /// <summary>
        /// Gets or sets the property list.
        /// </summary>
        /// <value>
        /// The property list.
        /// </value>
        [XmlElement(ElementName = "property")]
        public List<Property> PropertyList { get; set; }
    }
}
