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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Engine;
using NUnit.Framework;
using YellowJacket.Common.Helpers;
using YellowJacket.Core.NUnitWrapper.Models;

namespace YellowJacket.Core.NUnitWrapper
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
        /// <param name="assemblies">The list of assemblies.</param>
        /// <returns><see cref="TestPackage"/>.</returns>
        public static TestPackage CreateTestPackage(IList<string> assemblies)
        {
            TestPackage testPackage = new TestPackage(assemblies);

            // TODO Need to convert those hardcoded values to constants
            testPackage.AddSetting("ProcessModel", "Single");
            testPackage.AddSetting("DomainUsage", "None");

            return testPackage;
        }

        /// <summary>
        /// Creates the test filter.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="features">The features.</param>
        /// <returns><see cref="TestFilter"/>.</returns>
        public static TestFilter CreateTestFilter(Assembly assembly, List<string> features)
        {
            ITestFilterBuilder filterBuilder = new TestFilterBuilder();

            List<string> filters = features.Select(feature => 
                $"class = {TypeLocatorHelper.GetTypeByAttributeAndName(assembly, typeof(DescriptionAttribute), feature)}")
                    .ToList();

            filterBuilder.SelectWhere(string.Join(" || ", filters));

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

        /// <summary>
        /// Parses the test case.
        /// </summary>
        /// <param name="xmlFragment">The XML fragment.</param>
        /// <returns><see cref="TestCase"/>.</returns>
        public static TestCase ParseTestCase(string xmlFragment)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TestCase));

            TestCase testCase;

            using (TextReader reader = new StringReader(xmlFragment))
            {
                testCase = (TestCase)serializer.Deserialize(reader);
            }

            return testCase;
        }

        #endregion
    }
}
