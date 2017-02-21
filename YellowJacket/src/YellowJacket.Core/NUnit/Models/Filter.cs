using System.Xml.Serialization;

namespace YellowJacket.Core.NUnit.Models
{
    /// <summary>
    /// Represents a NUnit test filter.
    /// </summary>
    [XmlRoot(ElementName = "filter")]
    public class Filter
    {
        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        /// <value>
        /// The test.
        /// </value>
        [XmlElement(ElementName = "test")]
        public Test Test { get; set; }
    }
}
