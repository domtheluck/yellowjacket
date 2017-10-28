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
    /// Represents an NUnit test run.
    /// </summary>
    [XmlRoot(ElementName = "test-run")]
    public class TestRun
    {
        /// <summary>
        /// Gets or sets the command line.
        /// </summary>
        /// <value>
        /// The command line.
        /// </value>
        [XmlElement(ElementName = "command-line")]
        public string CommandLine { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        [XmlElement(ElementName = "filter")]
        public Filter Filter { get; set; }

        /// <summary>
        /// Gets or sets the test suite.
        /// </summary>
        /// <value>
        /// The test suite.
        /// </value>
        [XmlElement(ElementName = "test-suite")]
        public TestSuite TestSuite { get; set; }

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
        /// Gets or sets the test case count.
        /// </summary>
        /// <value>
        /// The test case count.
        /// </value>
        [XmlAttribute(AttributeName = "testcasecount")]
        public int TestCaseCount { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        [XmlAttribute(AttributeName = "result")]
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [XmlAttribute(AttributeName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the passed.
        /// </summary>
        /// <value>
        /// The passed.
        /// </value>
        [XmlAttribute(AttributeName = "passed")]
        public int Passed { get; set; }

        /// <summary>
        /// Gets or sets the failed.
        /// </summary>
        /// <value>
        /// The failed.
        /// </value>
        [XmlAttribute(AttributeName = "failed")]
        public int Failed { get; set; }

        /// <summary>
        /// Gets or sets the inconclusive.
        /// </summary>
        /// <value>
        /// The inconclusive.
        /// </value>
        [XmlAttribute(AttributeName = "inconclusive")]
        public int Inconclusive { get; set; }

        /// <summary>
        /// Gets or sets the skipped.
        /// </summary>
        /// <value>
        /// The skipped.
        /// </value>
        [XmlAttribute(AttributeName = "skipped")]
        public int Skipped { get; set; }

        /// <summary>
        /// Gets or sets the asserts.
        /// </summary>
        /// <value>
        /// The asserts.
        /// </value>
        [XmlAttribute(AttributeName = "asserts")]
        public int Asserts { get; set; }

        /// <summary>
        /// Gets or sets the engine version.
        /// </summary>
        /// <value>
        /// The engine version.
        /// </value>
        [XmlAttribute(AttributeName = "engine-version")]
        public string EngineVersion { get; set; }

        /// <summary>
        /// Gets or sets the CLR version.
        /// </summary>
        /// <value>
        /// The CLR version.
        /// </value>
        [XmlAttribute(AttributeName = "clr-version")]
        public string ClrVersion { get; set; }

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
    }
}
