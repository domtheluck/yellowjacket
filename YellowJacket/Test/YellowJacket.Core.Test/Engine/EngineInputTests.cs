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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NUnit.Framework;
using YellowJacket.Core.Engine;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Factories;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Test.Engine
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class EngineInputTests
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
        public void Initialize_NotCreated_NoError()
        {
            // Arrange

            // Act
            IEngine engine = EngineFactory.Create();

            // Assert
            Assert.That(engine, !Is.Null, "The IEngine instance should not be null.");
        }

        [Test]
        public void Run_NoTestAssemblyFullNameSpecified_ArgumentExceptionThrown()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();
            Configuration configuration = new Configuration();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() 
                => engine.Run(configuration), "The TestAssemblyFullName must not be empty.");
        }

        [Test]
        public void Run_InvalidTestAssemblyLocation_IOExceptionThrown()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();
            const string testAssemblyFullName = "MyTestAssembly.dll";

            // Act

            Configuration configuration = 
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName
                };

            // Assert
            Assert.Throws<IOException>(() 
                => engine.Run(configuration), $"The TestAssemblyFullName {testAssemblyFullName} must contains the path.");
        }

        [Test]
        public void Run_InvalidTestAssembly_IOExceptionThrown()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();
            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyTestAssembly.dll");

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName
                };

            // Assert
            Assert.Throws<IOException>(()
                => engine.Run(configuration), $"The TestAssemblyFullName {testAssemblyFullName} is invalid.");
        }

        [Test]
        public void Run_ValidConfigurationSingleFeature_NoError()
        {
            // Arrange
            IEngine engine = EngineFactory.Create();

            string testAssemblyFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YellowJacket.Core.Test.Data.dll");

            List<string> features = new List<string> {"Login"};

            // Act
            Configuration configuration =
                new Configuration
                {
                    TestAssemblyFullName = testAssemblyFullName,
                    Features = features
                };

            engine.Run(configuration);

            // Assert
            Assert.That(engine.Status, Is.EqualTo(EngineStatus.Completed));
        }

        #endregion
    }
}
