using System.Xml.Serialization;

namespace YellowJacket.Core.NUnit.Models
{
    /// <summary>
    /// Represents the NUnit environment.
    /// </summary>
    [XmlRoot(ElementName = "environment")]
    public class Environment
    {
        /// <summary>
        /// Gets or sets the framework version.
        /// </summary>
        /// <value>
        /// The framework version.
        /// </value>
        [XmlAttribute(AttributeName = "framework-version")]
        public string FrameworkVersion { get; set; }

        /// <summary>
        /// Gets or sets the CLR version.
        /// </summary>
        /// <value>
        /// The CLR version.
        /// </value>
        [XmlAttribute(AttributeName = "clr-version")]
        public string CLRVersion { get; set; }

        /// <summary>
        /// Gets or sets the os version.
        /// </summary>
        /// <value>
        /// The os version.
        /// </value>
        [XmlAttribute(AttributeName = "os-version")]
        public string OSVersion { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>
        /// The platform.
        /// </value>
        [XmlAttribute(AttributeName = "platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the CWD.
        /// </summary>
        /// <value>
        /// The CWD.
        /// </value>
        [XmlAttribute(AttributeName = "cwd")]
        public string Cwd { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>
        /// The name of the machine.
        /// </value>
        [XmlAttribute(AttributeName = "machine-name")]
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [XmlAttribute(AttributeName = "user")]
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the user domain.
        /// </summary>
        /// <value>
        /// The user domain.
        /// </value>
        [XmlAttribute(AttributeName = "user-domain")]
        public string UserDomain { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        [XmlAttribute(AttributeName = "culture")]
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the UI culture.
        /// </summary>
        /// <value>
        /// The UI culture.
        /// </value>
        [XmlAttribute(AttributeName = "uiculture")]
        public string UICulture { get; set; }

        /// <summary>
        /// Gets or sets the os architecture.
        /// </summary>
        /// <value>
        /// The os architecture.
        /// </value>
        [XmlAttribute(AttributeName = "os-architecture")]
        public string OSArchitecture { get; set; }
    }
}
