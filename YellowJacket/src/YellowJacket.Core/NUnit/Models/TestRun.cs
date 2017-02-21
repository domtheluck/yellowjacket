using System.Xml.Serialization;

namespace YellowJacket.Core.NUnit.Models
{
    /// <summary>
    /// Represents an NUnit test run.
    /// </summary>
    [XmlRoot(ElementName = "test-run")]
    public class TestRun
    {
        [XmlElement(ElementName = "command-line")]
        public string Commandline { get; set; }

        [XmlElement(ElementName = "filter")]
        public Filter Filter { get; set; }

        [XmlElement(ElementName = "test-suite")]
        public TestSuite TestSuite { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "fullname")]
        public string Fullname { get; set; }

        [XmlAttribute(AttributeName = "testcasecount")]
        public string Testcasecount { get; set; }

        [XmlAttribute(AttributeName = "result")]
        public string Result { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }

        [XmlAttribute(AttributeName = "passed")]
        public string Passed { get; set; }

        [XmlAttribute(AttributeName = "failed")]
        public string Failed { get; set; }

        [XmlAttribute(AttributeName = "inconclusive")]
        public string Inconclusive { get; set; }

        [XmlAttribute(AttributeName = "skipped")]
        public string Skipped { get; set; }

        [XmlAttribute(AttributeName = "asserts")]
        public string Asserts { get; set; }

        [XmlAttribute(AttributeName = "engine-version")]
        public string Engineversion { get; set; }

        [XmlAttribute(AttributeName = "clr-version")]
        public string Clrversion { get; set; }

        [XmlAttribute(AttributeName = "start-time")]
        public string Starttime { get; set; }

        [XmlAttribute(AttributeName = "end-time")]
        public string Endtime { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }
    }
}
