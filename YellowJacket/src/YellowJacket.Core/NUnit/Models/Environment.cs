// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

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
        /// Gets or sets the OS version.
        /// </summary>
        /// <value>
        /// The OS version.
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
