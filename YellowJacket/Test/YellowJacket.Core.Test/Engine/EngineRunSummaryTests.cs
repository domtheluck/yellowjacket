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
    public class EngineRunSummaryTests
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
        public void RunSummary_FeatureCount_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Register" };

            const int featureCount = 2;

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.FeatureCount,
                Is.EqualTo(featureCount),
                $"The actual feature count {engine.RunSummary.FeatureCount} is not equal to the expected one {featureCount}.");
        }

        [Test]
        public void RunSummary_ScenarioCount_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Register" };

            const int scenarioCount = 3;

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.ScenarioCount,
                Is.EqualTo(scenarioCount),
                $"The actual scenario count {engine.RunSummary.ScenarioCount} is not equal to the expected one {scenarioCount}.");
        }

        [Test]
        public void RunSummary_StepCount_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Register" };

            const int stepCount = 11;

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.StepCount,
                Is.EqualTo(stepCount),
                $"The actual step count {engine.RunSummary.StepCount} is not equal to the expected one {stepCount}.");
        }

        [Test]
        public void RunSummary_PreviousFeature_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            const string previousFeatureName = "Navigate";

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.PreviousFeature.Name,
                Is.EqualTo(previousFeatureName),
                $"The actual previous feature name {engine.RunSummary.PreviousFeature.Name} is not equal to the expected one {previousFeatureName}.");
        }

        [Test]
        public void RunSummary_CurrentFeature_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            const string currentFeatureName = "Register";

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.CurrentFeature.Name,
                Is.EqualTo(currentFeatureName),
                $"The actual current feature name {engine.RunSummary.CurrentFeature.Name} is not equal to the expected one {currentFeatureName}.");
        }

        [Test]
        public void RunSummary_NextFeature_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.NextFeature,
                Is.Null,
                "The actual next feature is not null.");
        }

        [Test]
        public void RunSummary_PreviousScenario_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            const string previousScenarioName = "Navigate Profile Page";

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.PreviousScenario.Name,
                Is.EqualTo(previousScenarioName),
                $"The actual previous scenario name {engine.RunSummary.PreviousScenario.Name} is not equal to the expected one {previousScenarioName}.");
        }

        [Test]
        public void RunSummary_CurrentScenario_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            const string currentScenarioName = "Register Success";

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.CurrentScenario.Name,
                Is.EqualTo(currentScenarioName),
                $"The actual current scenario name {engine.RunSummary.CurrentScenario.Name} is not equal to the expected one {currentScenarioName}.");
        }

        [Test]
        public void RunSummary_NextScenario_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.NextScenario,
                Is.Null,
                "The actual next scenario is not null.");
        }

        [Test]
        public void RunSummary_PreviousStep_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            const string previousStepText = "I click 'Register' button";

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.PreviousStep.Text,
                Is.EqualTo(previousStepText),
                $"The actual previous step name {engine.RunSummary.PreviousStep.Text} is not equal to the expected one {previousStepText}.");
        }

        [Test]
        public void RunSummary_CurrentStep_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            const string currentStepName = "I see register success message";

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.CurrentStep.Text,
                Is.EqualTo(currentStepName),
                $"The actual current step name {engine.RunSummary.CurrentStep.Text} is not equal to the expected one {currentStepName}.");
        }

        [Test]
        public void RunSummary_NextStep_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Navigate", "Register" };

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(
                engine.RunSummary.NextStep,
                Is.Null,
                "The actual next step is not null.");
        }

        [Test]
        public void RunSummary_FeatureExecutionPercentage_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Register", "Navigate" };

            List<double> progressValues = new List<double>();

            List<double> expectedValues = new List<double>{0d, 33.33d, 66.67d, 100d};

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.ExecutionProgress += (sender, e) =>
            {
                if (!progressValues.Contains(e.RunSummary.FeatureExecutionPercentage))
                    progressValues.Add(e.RunSummary.FeatureExecutionPercentage);

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
        public void RunSummary_ScenarioExecutionPercentage_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> { "Login", "Register", "Navigate" };

            List<double> progressValues = new List<double>();

            List<double> expectedValues = new List<double> { 0d, 16.67d, 33.33d, 50d, 66.67d, 83.33d, 100d};

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.ExecutionProgress += (sender, e) =>
            {
                if (!progressValues.Contains(e.RunSummary.ScenarioExecutionPercentage))
                    progressValues.Add(e.RunSummary.ScenarioExecutionPercentage);

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
        public void RunSummary_StepExecutionPercentage_NoError()
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

            engine.ExecutionProgress += (sender, e) =>
            {
                if (!progressValues.Contains(e.RunSummary.StepExecutionPercentage))
                    progressValues.Add(e.RunSummary.StepExecutionPercentage);

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
