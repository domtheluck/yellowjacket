using System.Xml.Serialization;

namespace YellowJacket.Core.NUnit.Models
{
    /// <summary>
    /// Represents a NUnit test.
    /// </summary>
    [XmlRoot(ElementName = "test")]
    public class Test
    {
        /// <summary>
        /// Gets or sets the repetition.
        /// </summary>
        /// <value>
        /// The repetition.
        /// </value>
        [XmlAttribute(AttributeName = "re")]
        public string Re { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [XmlText]
        public string Text { get; set; }
    }
}
