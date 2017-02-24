﻿using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Engine;
using YellowJacket.Core.NUnit.Models;

namespace YellowJacket.Core.NUnit
{
    /// <summary>
    /// Helper class for the NUnit engine.
    /// </summary>
    internal class NUnitEngineHelper
    {
        #region Public Methods

        /// <summary>
        /// Creates the test package.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns><see cref="TestPackage"/>.</returns>
        public static TestPackage CreateTestPackage(IList<string> assemblies)
        {
            return new TestPackage(assemblies);
        }

        /// <summary>
        /// Creates the test filter.
        /// </summary>
        /// <param name="feature">The feature.</param>
        /// <returns><see cref="TestFilter"/>.</returns>
        public static TestFilter CreateTestFilter(string feature)
        {
            ITestFilterBuilder filterBuilder = new TestFilterBuilder();

            // TODO: maybe not the best way to filter the tests. We might need to revisit this.
            filterBuilder.SelectWhere($"test =~ {feature}Feature");

            return filterBuilder.GetFilter();
        }

        /// <summary>
        /// Parses the test run.
        /// </summary>
        /// <param name="xmlFragment">The XML fragment.</param>
        /// <returns><see cref="TestRun"/>.</returns>
        public static TestRun ParseTestRun(XmlNode xmlFragment)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TestRun));

            TestRun testRun;

            using (TextReader reader = new StringReader(xmlFragment.OuterXml))
            {
                testRun = (TestRun)serializer.Deserialize(reader);
            }

            return testRun;
        }

        /// <summary>
        /// Parses the test suite.
        /// </summary>
        /// <param name="xmlFragment">The XML fragment.</param>
        /// <returns><see cref="TestSuite"/>.</returns>
        public static TestSuite ParseTestSuite(XmlNode xmlFragment)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TestSuite));

            TestSuite testSuite;

            using (TextReader reader = new StringReader(xmlFragment.OuterXml))
            {
                testSuite = (TestSuite)serializer.Deserialize(reader);
            }

            return testSuite;
        }

        #endregion
    }
}
