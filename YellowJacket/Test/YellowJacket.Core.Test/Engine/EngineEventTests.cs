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
    [Parallelizable(ParallelScope.All)]
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

        #endregion
    }
}
