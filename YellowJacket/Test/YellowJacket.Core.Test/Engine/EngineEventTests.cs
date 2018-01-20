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

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using YellowJacket.Core.Engine;
using YellowJacket.Core.Factories;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Test.Engine
{
    [TestFixture]
    [Category("Engine")]

    public class EngineEventTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        #region Test Methods

        [Test]
        public void ExecutionStartEvent_ValidConfigurationSingleFeature_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login" };

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            bool eventFired = false;

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.ExecutionStart += (sender, e) =>
            {
                eventFired = true;
                autoResetEvent.Set();
            };

            engine.Run(configuration);

            // Assert
            Assert.That(autoResetEvent.WaitOne(), Is.True, "The ExecutionStart event have not been fired");
            Assert.That(eventFired, Is.EqualTo(true), "The ExecutionStart event have not been fired");
        }

        [Test]
        public void ExecutionCompletedEvent_ValidConfigurationSingleFeature_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login" };

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            bool eventFired = false;

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.ExecutionCompleted += (sender, e) =>
            {
                eventFired = true;
                autoResetEvent.Set();
            };

            engine.Run(configuration);

            // Assert
            Assert.That(autoResetEvent.WaitOne(), Is.True, "The ExecutionCompleted event have not been fired");
            Assert.That(eventFired, Is.EqualTo(true), "The ExecutionCompleted event have not been fired");
        }

        [Test]
        public void ExecutionProgressEvent_ValidConfigurationSingleFeature_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login" };

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            bool eventFired = false;

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.ExecutionProgress += (sender, e) =>
            {
                eventFired = true;
                autoResetEvent.Set();
            };

            engine.Run(configuration);

            // Assert
            Assert.That(autoResetEvent.WaitOne(), Is.True, "The ExecutionProgress event have not been fired");
            Assert.That(eventFired, Is.EqualTo(true), "The ExecutionProgress event have not been fired");
        }



        [Test]
        public void FeatureExecutionProgress_MultipleValidFeatures_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Register", "Navigate" };

            List<double> progressValues = new List<double>();

            List<double> expectedValues = new List<double> { 33.33d, 66.67d, 100d };

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.FeatureExecutionProgress += (sender, e) =>
            {
                if (!progressValues.Contains(e.ExecutionPercentage))
                    progressValues.Add(e.ExecutionPercentage);

                autoResetEvent.Set();
            };

            engine.Run(configuration);

            // Assert
            Assert.That(
                progressValues,
                Is.EqualTo(expectedValues),
                $"The actual feature progress values {string.Join(", ", progressValues)} are not equals to the expected ones {string.Join(", ", expectedValues)}");
        }

        [Test]
        public void ScenarioExecutionProgress_MultipleValidFeatures_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Register", "Navigate" };

            List<double> progressValues = new List<double>();

            List<double> expectedValues = new List<double> { 16.67d, 33.33d, 50d, 66.67d, 83.33d, 100d };

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.ScenarioExecutionProgress += (sender, e) =>
            {
                if (!progressValues.Contains(e.ExecutionPercentage))
                    progressValues.Add(e.ExecutionPercentage);

                autoResetEvent.Set();
            };

            engine.Run(configuration);

            // Assert
            Assert.That(
                progressValues,
                Is.EqualTo(expectedValues),
                $"The actual scenario progress values {string.Join(", ", progressValues)} are not equals to the expected ones {string.Join(", ", expectedValues)}");
        }

        [Test]
        public void StepExecutionProgress_MultipleValidFeatures_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Register", "Navigate" };

            List<double> progressValues = new List<double>();

            List<double> expectedValues =
                new List<double> { 5.88d, 11.76d, 17.65d, 23.53d, 29.41d, 35.29d, 41.18d, 47.06d, 52.94d, 58.82d, 64.71d, 70.59d, 76.47d, 82.35d, 88.24d, 94.12d, 100d };

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.StepExecutionProgress += (sender, e) =>
            {
                if (!progressValues.Contains(e.ExecutionPercentage))
                    progressValues.Add(e.ExecutionPercentage);

                autoResetEvent.Set();
            };

            engine.Run(configuration);

            // Assert
            Assert.That(
                progressValues,
                Is.EqualTo(expectedValues),
                $"The actual step progress values {string.Join(", ", progressValues)} are not equals to the expected ones {string.Join(", ", expectedValues)}");
        }



        #endregion
    }
}
