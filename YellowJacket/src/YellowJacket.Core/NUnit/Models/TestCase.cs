using System.Xml.Serialization;

namespace YellowJacket.Core.NUnit.Models
{
    /// <summary>
    /// Represents an NUnit test case.
    /// </summary>
    [XmlRoot(ElementName = "test-case")]
    public class TestCase
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        [XmlElement(ElementName = "properties")]
        public Properties Properties { get; set; }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>
        /// The output.
        /// </value>
        [XmlElement(ElementName = "output")]
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [XmlAttribute(AttributeName = "fullname")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>
        /// The name of the method.
        /// </value>
        [XmlAttribute(AttributeName = "methodname")]
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        [XmlAttribute(AttributeName = "classname")]
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the state of the run.
        /// </summary>
        /// <value>
        /// The state of the run.
        /// </value>
        [XmlAttribute(AttributeName = "runstate")]
        public string RunState { get; set; }

        /// <summary>
        /// Gets or sets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        [XmlAttribute(AttributeName = "seed")]
        public string Seed { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        [XmlAttribute(AttributeName = "result")]
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        [XmlAttribute(AttributeName = "start-time")]
        public string StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        [XmlAttribute(AttributeName = "end-time")]
        public string EndTime { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [XmlAttribute(AttributeName = "duration")]
        public double Duration { get; set; }

        /// <summary>
        /// Gets or sets the asserts.
        /// </summary>
        /// <value>
        /// The asserts.
        /// </value>
        [XmlAttribute(AttributeName = "asserts")]
        public int Asserts { get; set; }
    }
}
